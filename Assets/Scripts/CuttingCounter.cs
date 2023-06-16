using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
	[SerializeField] private CuttingObjectSO[] cuttingRecipesObjectSO; // all possible recipes


	public override void OnInteract(Player player)
	{
		if (!HasKitchenObject()) // check, if current counter has object on it
		{
			if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) //check, if player has something to put on cutting counter and if we actually can cut that
			{
				if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // check, if we have such recipe, to cut that into slices
				{
					player.GetKitchenObject().SetKitchenObjectParent(this); // place object from parent on counter, we are looking at
				}
			}
		}
		else // if that counter already has an object on it
		{
			if (player.HasKitchenObject()) // if player has object with it
			{
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
			if(HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) // to prevent cutting slices again
			{
				var kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());// get recipe from what is standing on cutting counter
				GetKitchenObject().DesroySelf(); // destroy unsliced object and make kitchenObjectParent forget about itself

				KitchenObject.SpawnKitchenObject(kitchenObjectSO, this); // spawned sliced objects
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
		foreach (CuttingObjectSO cuttingObjectSO in cuttingRecipesObjectSO)
		{
			if(cuttingObjectSO.input == inputKitchenObjectSO) // if we have such object in recipes, so true(we can cut that)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// From particular kitchenObject, we get its slices(if we can)
	/// </summary>
	/// <param name="kitchenObjectSO"></param>
	/// <returns></returns>
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO) 
	{
		foreach (CuttingObjectSO cuttingObjectSO in cuttingRecipesObjectSO) 
		{
			if(cuttingObjectSO.input == kitchenObjectSO) // if we have such recipe, we return that
			{
				return cuttingObjectSO.output;
			}
		}

		return null;
	}
}
