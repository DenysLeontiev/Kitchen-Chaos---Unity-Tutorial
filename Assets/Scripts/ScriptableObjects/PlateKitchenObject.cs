using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	// this event is called every time, we add new ingridient and also we know, which ingridient has been added
	public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded; 
	public class OnIngridientAddedEventArgs : EventArgs
	{
		public KitchenObjectSO kitchenObjectSO;
	}

	[SerializeField] private List<KitchenObjectSO> validIngridientsKitchenObjectSO;

    private List<KitchenObjectSO> kitchenObjectIngredients;

	private void Awake()
	{
		kitchenObjectIngredients = new List<KitchenObjectSO>();
	}

	public bool TryAddIndridient(KitchenObjectSO kitchenObjectSO)
	{
		if(!validIngridientsKitchenObjectSO.Contains(kitchenObjectSO))
		{
			return false;
		}
		if (kitchenObjectIngredients.Contains(kitchenObjectSO))
		{
			//Already in here
			return false;
		}
		else
		{
			kitchenObjectIngredients.Add(kitchenObjectSO);
			OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
			return true;
		}
	}

	public List<KitchenObjectSO> GetKitchenObjectSOsIngridients()
	{
		return kitchenObjectIngredients;
	}
}
