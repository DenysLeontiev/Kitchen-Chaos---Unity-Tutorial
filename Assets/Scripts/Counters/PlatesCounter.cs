using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlateRemoved;

	[SerializeField] private KitchenObjectSO plateKithcenObjectSO;

	private float spawnPlateTimer;
	private float spawnPlateRate = 4f;
	private int currentPlateAmount;
	private int maxPlateAmount = 4;

	private void Update()
	{
		spawnPlateTimer += Time.deltaTime;
		Debug.Log("spawnPlateRate: " + spawnPlateRate);
		if(spawnPlateTimer > spawnPlateRate) // spaw playe every 4 seconds(max 4 plates at once on PlatesCounter)
		{
			spawnPlateTimer = 0f;
			if(currentPlateAmount < maxPlateAmount)
			{
				Debug.Log("Spawned");
				currentPlateAmount++;
				OnPlateSpawned?.Invoke(this, EventArgs.Empty); // notify every listener(to update visual) that new plate has been created
			}
		}
	}

	public override void OnInteract(Player player)
	{
		if(!player.HasKitchenObject())
		{
			if(currentPlateAmount > 0)
			{
				KitchenObject.SpawnKitchenObject(plateKithcenObjectSO, player);
				currentPlateAmount--;
				OnPlateRemoved?.Invoke(this, EventArgs.Empty); // notify every listener(to update visual) that plate has been removed
			}
		}
	}
}
