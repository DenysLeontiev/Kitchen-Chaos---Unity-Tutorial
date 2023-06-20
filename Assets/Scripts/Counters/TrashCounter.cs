using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Counter for handling deletions of objects
/// </summary>
public class TrashCounter : BaseCounter
{
	public override void OnInteract(Player player)
	{
		if(player.HasKitchenObject()) // check if player has kitchenObject 
		{
			player.GetKitchenObject().DesroySelf(); // if player has, we delete that object
		}
	}
}
