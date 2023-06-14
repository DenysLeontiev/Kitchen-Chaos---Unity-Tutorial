using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO; // each counter knows which object it is spawning
    [SerializeField] private Transform counterTopPoint; // place, where we spawn kicthenObjects
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

	private KitchenObject kitchenObject; // so that the counter knows, if that has an object on it

	private void Update()
	{
		if(testing && Input.GetKeyDown(KeyCode.T)) // just for testing
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter); // just for testing, we send another clearCounter as parent to kitchenObject
            }
        }
	}

	public void OnInteract()
    {
        if (kitchenObject == null) // if we alredy have that
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint); // spawns at particular position

			// here, we set new clearCounter for kitchenObject,which is on it
			kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
		}
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());  // get kitchenObject`s clearCounter
        }

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

    public bool HasKithcenObject() // if curretn clearCounter has kitchenObject on it
    {
        return kitchenObject != null;
    }
}
