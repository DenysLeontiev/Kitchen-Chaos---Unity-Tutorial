using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatesIconSingleUI : MonoBehaviour
{
	[SerializeField] private Image iconImage;
	
	public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
	{
		iconImage.sprite = kitchenObjectSO.sprite;
	}
}
