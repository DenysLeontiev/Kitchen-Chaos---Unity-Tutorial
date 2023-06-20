using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private GameObject plateVisualPrefab; // just a visual thing without any scripts on it
	[SerializeField] private Transform counterTopPoint; // where we spawn that plates

	private List<GameObject> spawnedPlatesPrefabs;

	private void Awake()
	{
		spawnedPlatesPrefabs = new List<GameObject>();
	}

	void Start()
    {
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned; // we listen to event, which is called every time, new plate is spawned
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved; // we listen to event, which is called every time, plate is removed
}

	private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
	{
		// take the last plate from top and remove it and destroy
		GameObject lastPlatePrefabVisual = spawnedPlatesPrefabs[spawnedPlatesPrefabs.Count - 1]; 
		spawnedPlatesPrefabs.Remove(lastPlatePrefabVisual);
		Destroy(lastPlatePrefabVisual);
	}

	private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
	{
		// create dumb visual with counterTopPoint parent and position that in a center with small yOffset
		Transform plateVisualPrefabTransform = Instantiate(plateVisualPrefab, counterTopPoint).GetComponent<Transform>();
		float plateOffsetY = 0.1f;
		plateVisualPrefabTransform.localPosition = new Vector3(0, plateOffsetY * spawnedPlatesPrefabs.Count, 0);

		spawnedPlatesPrefabs.Add(plateVisualPrefabTransform.gameObject); // add new plate to our list of all current plates
	}
}
