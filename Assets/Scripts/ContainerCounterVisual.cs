using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles visual for animation(we separet visual from logic)
/// </summary>
public class ContainerCounterVisual : MonoBehaviour
{
	private const string OPEN_CLOSE = "OpenClose";

	[SerializeField] private ContainerCounter containerCounter; // our parent

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		// we subscribe to event, whick is triggered every time, player grabs an item
		containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject; 
	}

	private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
	{
		animator.SetTrigger(OPEN_CLOSE); // play an animation(opens a counter)
	}
}
