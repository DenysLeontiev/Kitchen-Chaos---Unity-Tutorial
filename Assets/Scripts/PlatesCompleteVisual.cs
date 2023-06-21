using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCompleteVisual : MonoBehaviour
{
	/// <summary>
	/// struct,which has kitchenObjectSO to identify objects and kitchenObjectVisual to set their visual
	/// </summary>
	[Serializable]
	private struct KitchenObjectSO_GameObject
	{
		public GameObject kitchenObjectVisual;
		public KitchenObjectSO kitchenObjectSO;
	}

	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectVisualList;

    private void Start()
    {
		plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;

		foreach (KitchenObjectSO_GameObject kitchenObject in kitchenObjectVisualList)
		{
			kitchenObject.kitchenObjectVisual.SetActive(false);
		}
    }

	private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
	{
		foreach (KitchenObjectSO_GameObject kitchenObject in kitchenObjectVisualList)
		{
			if(kitchenObject.kitchenObjectSO == e.kitchenObjectSO) // if somethig, that we have added can be on burger, we display that on burger
			{
				kitchenObject.kitchenObjectVisual.SetActive(true);
			}
		}
	}
}
