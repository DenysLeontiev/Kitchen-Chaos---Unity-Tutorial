using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
	private const string OPEN_CLOSE = "OpenClose";

	public event EventHandler OnPlayerGrabbedObject; // we call that event, every time, player gtrabs an object

	[SerializeField] private KitchenObjectSO kitchenObjectSO; // each counter knows which object it is spawning

	public override void OnInteract(Player player)
	{
		if(!player.HasKitchenObject()) // player is not carrying any object
		{
			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty); // we notify all the listeners,that player grabbed an object
			Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); // spawns at particular position

			// here, we set new clearCounter for kitchenObject,which is on it
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player); // we direwctly give that(kitchenObject) to the player
		}
		
	}
}
