using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public class OnStateChangedEventArgs 
	{
		public State state;
	}

	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
	[SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

	public enum State
	{
		Idle,
		Frying,
		Fried,
		Burned,
	}

	private FryingRecipeSO fryingRecipeSO;
	private BurningRecipeSO burningRecipeSO;

	private float fryingTimer;
	private float burningTimer;
	private State currentState;

	private void Start()
	{
		currentState = State.Idle;
	}

	private void Update()
	{
		if(HasKitchenObject())
		{
			switch (currentState)
			{
				case State.Idle:
					break;
				case State.Frying:
					fryingTimer += Time.deltaTime;
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });

					if (fryingTimer > fryingRecipeSO.fryingTimerMax)
					{
						GetKitchenObject().DesroySelf();

						KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
						
						burningRecipeSO = GetBurningObjectSO(GetKitchenObject().GetKitchenObjectSO());

						burningTimer = 0f;
						currentState = State.Fried;

						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });

					}
					break;
				case State.Fried:
					burningTimer += Time.deltaTime;
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = burningTimer / burningRecipeSO.burningTimerMax});

					if (burningTimer > burningRecipeSO.burningTimerMax)
					{
						GetKitchenObject().DesroySelf();

						KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

						burningRecipeSO = GetBurningObjectSO(GetKitchenObject().GetKitchenObjectSO());

						currentState = State.Burned;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });

					}
					break;
				case State.Burned:
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = 0f });

					break;
			}
		}
	}

	public override void OnInteract(Player player)
	{
		if (!HasKitchenObject()) // check, if current counter has object on it
		{
			if (player.HasKitchenObject()) //check, if player has something to put on cutting counter and if we actually can cut that
			{
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // check, if we have such recipe, to cut that into slices
				{
					player.GetKitchenObject().SetKitchenObjectParent(this); // place object from parent on counter, we are looking at
					
					fryingRecipeSO = GetFryingObjectSO(GetKitchenObject().GetKitchenObjectSO()); // here we get what we are going to fry
					fryingTimer = 0f;
					currentState= State.Frying;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });
				}
			}
		}
		else // if that counter already has an object on it
		{
			if (player.HasKitchenObject()) // if player has object with it
			{
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					if (plateKitchenObject.TryAddIndridient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DesroySelf();

						currentState = State.Idle;

						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = 0f });
					}
				}
			}
			else // if player does not have object with it, we jus	t add this to the player from counter
			{
				GetKitchenObject().SetKitchenObjectParent(player);// here, we get kitchenObject on this ClearCounter and make player is its new parent
				currentState = State.Idle;

				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = 0f });


			}
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		FryingRecipeSO cuttingObjectSO = GetFryingObjectSO(inputKitchenObjectSO);

		return cuttingObjectSO != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
	{
		FryingRecipeSO cuttingObjectSO = GetFryingObjectSO(kitchenObjectSO);

		if (cuttingObjectSO != null)
		{
			return cuttingObjectSO.output;
		}
		else
		{
			return null;
		}
	}

	private FryingRecipeSO GetFryingObjectSO(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
		{
			if (inputKitchenObjectSO == fryingRecipeSO.input)
			{
				return fryingRecipeSO;
			}
		}

		return null;
	}

	private BurningRecipeSO GetBurningObjectSO(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
		{
			if (inputKitchenObjectSO == burningRecipeSO.input)
			{
				return burningRecipeSO;
			}
		}

		return null;
	}
}
