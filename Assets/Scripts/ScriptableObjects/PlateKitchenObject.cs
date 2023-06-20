using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
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
			return true;
		}
	}
}
