using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Here we store all KitchenObject's data
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab; // object's prefab to create on intearaction
    public Sprite sprite; // object's sprite 
    public string objectName; // object's name
}
