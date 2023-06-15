using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KitchenObject : MonoBehaviour
{
    // So that the object knows its own type, properties, name etc(knows who it is)
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent; // so that the object knows its parent(where it is standing)

    // this method returns its data 
    public KitchenObjectSO GetKitchenObjectSO() 
    {
        return kitchenObjectSO; 
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) // method,that sets new clearCounter for kithcenObject
	{
        if(this.kitchenObjectParent != null) // check if we have parent
        {
            this.kitchenObjectParent.ClearKitchenObject(); // we delete current kitchenObject from old parent
        }


        this.kitchenObjectParent = kitchenObjectParent;  // here we set new clearCounter for current kithcenObject

        if(kitchenObjectParent.HasKithcenObject()) // check,if that parent alredy has a kitchenObject on it
        {
            Debug.LogError("This counter alredy has kitchen object on it");
        }

        kitchenObjectParent.SetKitchenObject(this); // set curretn KitchenObject as child to clearCounter (new parent)
        transform.parent = kitchenObjectParent.GetKicthenObjectFollowTransform(); // here we set new parent for kithcenObject
		transform.localPosition = Vector3.zero; // make that be in the center
    }

    public IKitchenObjectParent GetClearCounter() // returning kitchenObject`s clearCounter
    {
        return kitchenObjectParent;
    }
}
