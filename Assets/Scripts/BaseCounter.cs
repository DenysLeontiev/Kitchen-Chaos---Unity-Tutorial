using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform counterTopPoint; // place, where we spawn kicthenObjects

	private KitchenObject kitchenObject;

	public virtual void OnInteractAlternate(Player player)
	{

	}

	public virtual void OnInteract(Player player)
	{
		
	}

	public Transform GetKicthenObjectFollowTransform() // get kithenObject`s parent
	{
		return counterTopPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) // set for current clearCounter new kitchenObject
	{
		this.kitchenObject = kitchenObject;
	}

	public KitchenObject GetKitchenObject() // return current clearCounter`s kitchenObject on it
	{
		return kitchenObject;
	}

	public void ClearKitchenObject() // make kitchenObject on clearCounter to be equeal to null
	{
		kitchenObject = null;
	}

	public bool HasKitchenObject() // if curretn clearCounter has kitchenObject on it
	{
		return kitchenObject != null;
	}
}
