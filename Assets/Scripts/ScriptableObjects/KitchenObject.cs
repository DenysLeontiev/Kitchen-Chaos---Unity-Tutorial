using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KitchenObject : MonoBehaviour
{
    // So that the object knows its own type, properties, name etc(knows who it is)
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter; // so that the object knows its parent(where it is standing)

    // this method returns its data 
    public KitchenObjectSO GetKitchenObjectSO() 
    {
        return kitchenObjectSO; 
    }

    public void SetClearCounter(ClearCounter clearCounter) // method,that sets new clearCounter for kithcenObject
	{
        if(this.clearCounter != null) // check if we have parent
        {
            this.clearCounter.ClearKitchenObject(); // we delete current kitchenObject from old parent
        }


        this.clearCounter = clearCounter;  // here we set new clearCounter for current kithcenObject

        if(clearCounter.HasKithcenObject()) // check,if that parent alredy has a kitchenObject on it
        {
            Debug.LogError("This counter alredy has kitchen object on it");
        }

        clearCounter.SetKitchenObject(this); // set curretn KitchenObject as child to clearCounter (new parent)
        transform.parent = clearCounter.GetKicthenObjectFollowTransform(); // here we set new parent for kithcenObject
		transform.localPosition = Vector3.zero; // make that be in the center
    }

    public ClearCounter GetClearCounter() // returning kitchenObject`s clearCounter
    {
        return clearCounter;
    }
}
