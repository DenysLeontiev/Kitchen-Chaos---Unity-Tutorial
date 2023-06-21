using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private Transform iconTemplate; // here we acces image to set the sprite

	private void Awake()
	{
		iconTemplate.gameObject.SetActive(false);
	}

	private void Start()
    {
		plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
    }

	private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
	{
		UpdateVisuals();
	}

	private void UpdateVisuals()
	{
		// here we cycle through all children of current transform to delete the old one and then we spawn new ones
		foreach (Transform child in transform)
		{
			//if thats our template, we dont delete that, because we want to have a reference to that in order to know how the created icon should look like
			if (child == iconTemplate) 
			{
				continue;
			}
			
			Destroy(child.gameObject);
		}

		foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOsIngridients())
		{
			Transform iconImageTransform = Instantiate(iconTemplate, transform);
			iconImageTransform.gameObject.SetActive(true);
			iconImageTransform.GetComponent<PlatesIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO); // here we set particular sprite for newly created iconImageTransform
		}
	}
}
