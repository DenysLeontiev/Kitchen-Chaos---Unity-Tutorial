using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
	// we listen to that, when we have cut something and also we send our current cutting progress
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
	public class OnProgressChangedEventArgs
	{
		public float cuttingProgressNormalized;
	}

	// this event is called when we cut an object(we want to play animation)
	public event EventHandler OnCut;

	[SerializeField] private CuttingObjectSO[] cuttingRecipesObjectSO; // all possible recipes
	private int cuttingProgress;


	public override void OnInteract(Player player)
	{
		if (!HasKitchenObject()) // check, if current counter has object on it
		{
			if (player.HasKitchenObject()) //check, if player has something to put on cutting counter and if we actually can cut that
			{
				if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // check, if we have such recipe, to cut that into slices
				{
					player.GetKitchenObject().SetKitchenObjectParent(this); // place object from parent on counter, we are looking at
					cuttingProgress = 0; // we set that to zero, so we can cut that again

					CuttingObjectSO cuttingObjectSO = GetCuttingObjectSO(GetKitchenObject().GetKitchenObjectSO());
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = (float)cuttingProgress /  cuttingObjectSO.maxCuttingAmount });
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
					}
				}
			}
			else // if player does not have object with it, we jus	t add this to the player from counter
			{
				GetKitchenObject().SetKitchenObjectParent(player);// here, we get kitchenObject on this ClearCounter and make player is its new parent
			}
		}
	}

	public override void OnInteractAlternate(Player player)
	{
		if(HasKitchenObject()) // if we have smth to slice
		{
			if(HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) // to prevent cutting slices again, we check is such reciept exists
			{
				cuttingProgress++;
				CuttingObjectSO cuttingObjectSO = GetCuttingObjectSO(GetKitchenObject().GetKitchenObjectSO());
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { cuttingProgressNormalized = (float)cuttingProgress /  cuttingObjectSO.maxCuttingAmount });

				OnCut?.Invoke(this, EventArgs.Empty);

				//if we have cut particular object more than maxCuttingAmount times,we get it sliced objects 
				if (cuttingProgress >= GetCuttingObjectSO(GetKitchenObject().GetKitchenObjectSO()).maxCuttingAmount)
				{
					var kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());// get recipe from what is standing on cutting counter
					GetKitchenObject().DesroySelf(); // destroy unsliced object and make kitchenObjectParent forget about itself

					KitchenObject.SpawnKitchenObject(kitchenObjectSO, this); // spawned sliced objects
				}
			}	
		}
	}

	/// <summary>
	/// This method checks,whether we can cut particular kitchenObject
	/// </summary>
	/// <param name="inputKitchenObjectSO"></param>
	/// <returns></returns>
	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		CuttingObjectSO cuttingObjectSO = GetCuttingObjectSO(inputKitchenObjectSO);

		return cuttingObjectSO != null;
	}

	/// <summary>
	/// From particular kitchenObject, we get its slices(if we can)
	/// </summary>
	/// <param name="kitchenObjectSO"></param>
	/// <returns></returns>
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO) 
	{
		CuttingObjectSO cuttingObjectSO = GetCuttingObjectSO(kitchenObjectSO);

		if(cuttingObjectSO != null)
		{
			return cuttingObjectSO.output;
		}
		else
		{
			return null;
		}
	}

	private CuttingObjectSO GetCuttingObjectSO(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (CuttingObjectSO cuttingObjectSO in cuttingRecipesObjectSO)
		{
			if(inputKitchenObjectSO == cuttingObjectSO.input)
			{
				return cuttingObjectSO;
			}
		}

		return null;
	}


}
