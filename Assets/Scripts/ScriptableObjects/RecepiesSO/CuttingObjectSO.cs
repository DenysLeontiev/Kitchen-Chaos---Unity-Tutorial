using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingObjectSO : ScriptableObject
{
	public KitchenObjectSO input; // what we get slices from(for example - tomato)
	public KitchenObjectSO output; // slices we got from tomato
}
