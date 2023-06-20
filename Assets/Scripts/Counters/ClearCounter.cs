using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO; // each counter knows which object it is spawning

	public override void OnInteract(Player player)
    {
		if(!HasKitchenObject()) // check, if current counter has object on it
		{
			if (player.HasKitchenObject()) //check, if player has something to put on counter
			{
				player.GetKitchenObject().SetKitchenObjectParent(this); // place object from parent on counter, we are looking at
			}
		}
		else // if that counter already has an object on it
		{
			if(player.HasKitchenObject()) // if player has object with it
			{
				if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					if(plateKitchenObject.TryAddIndridient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DesroySelf();
					}
				}
				else
				{
					// player is not carrying a Plate but something else
					if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) // counter is holding a plate
					{
						if (plateKitchenObject.TryAddIndridient(player.GetKitchenObject().GetKitchenObjectSO()))
						{
							player.GetKitchenObject().DesroySelf();
						}
					}
				}
			}
			else // if player does not have object with it, we jus	t add this to the player from counter
			{
				GetKitchenObject().SetKitchenObjectParent(player);// here, we get kitchenObject on this ClearCounter and make player is its new parent
			}
		}
    }
}
