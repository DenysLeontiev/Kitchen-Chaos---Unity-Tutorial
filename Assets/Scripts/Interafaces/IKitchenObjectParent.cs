using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
	public Transform GetKicthenObjectFollowTransform(); // get kithenObject`s parent

	public void SetKitchenObject(KitchenObject kitchenObject); // set for current clearCounter new kitchenObject

	public KitchenObject GetKitchenObject(); // return current clearCounter`s kitchenObject on it;

	public void ClearKitchenObject(); // make kitchenObject on clearCounter to be equeal to null;

	public bool HasKithcenObject(); // if curretn clearCounter has kitchenObject on it;
}
