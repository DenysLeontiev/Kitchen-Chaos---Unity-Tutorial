using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
	public static Player Instance { get; private set; }

	// When we have new selected counter, we notify listeners, that we have a new one
	// and also we pass that new selectedCounter as parameter
	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs // args, we pass to OnSelectedCounterChanged event
	{
		public ClearCounter selectedCounter;
	}


	[SerializeField] private GameInput gameInput;
	[SerializeField] private float moveSpeed = 1f;

	[SerializeField] private LayerMask countersLayerMask;// so we only can interacrt with counters

	private bool isWalking;
	private Vector3 lastMovementDirection;

	private ClearCounter selectedCounter;

	[SerializeField] private Transform kitchenObjectHoldPoint;
	private KitchenObject kitchenObject;

	private void Awake()
	{
		if(Instance != null)
		{
			Debug.Log("There is more that on eplayer instance!");
			//Destroy(this);
		}
		else 
		{
			Instance = this;
		}
	}

	private void Start()
	{
		gameInput.OnInteract += GameInput_OnInteract; // subsribe to that event
	}

	// this is called, when OnInteract(from GameInput class) event is fired('E' is pressed)
	private void GameInput_OnInteract(object sender, System.EventArgs e) 
	{
		if(selectedCounter != null)
		{
			selectedCounter.OnInteract(this);
		}
	}

	private void Update()
	{
		HandleMovement();
		HandleInteraction();
	}

	private void HandleMovement()
	{
		Vector2 inputVector = gameInput.GetVectorMovementNormalized(); // we ger normalized vector

		Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y); // moveDirection based on input

		isWalking = moveDirection != Vector3.zero; // no input == moving

		float playerHeight = 2f;
		float playerRadius = 0.7f;
		float moveDistance = moveSpeed * Time.deltaTime;

		// Check whether we can move forward
		bool canMove = !Physics.CapsuleCast(transform.position,
											transform.position + Vector3.up * playerHeight,
											playerRadius,
											moveDirection,
											moveDistance);
		if (!canMove)
		{
			// Chech the X-Movement(left,right)
			Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;

			canMove = !Physics.CapsuleCast(transform.position,
											transform.position + Vector3.up * playerHeight,
											playerRadius,
											moveDirectionX,
											moveDistance);
			if (canMove)
			{
				// Move along the x-axis
				moveDirection = moveDirectionX;
			}
			else
			{
				//Check the Z-Movement(up, down)
				Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;

				canMove = !Physics.CapsuleCast(transform.position,
												transform.position + Vector3.up * playerHeight,
												playerRadius,
												moveDirectionZ,
												moveDistance);
				if (canMove)
				{
					// Move along the Z-Axis
					moveDirection = moveDirectionZ;
				}
				else
				{
					// We can1t move in any direction
				}
			}
		}

		if (canMove)
		{
			// Here we can move in any direction
			transform.position += moveDirection * moveDistance;
		}

		float rotationSpeed = 10f;
		transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime); // Rotate player`s face to the side,we are moving to(Slerp for rotation, Lerp for moving)
	}

	private void HandleInteraction()
	{
		Vector2 inputVector = gameInput.GetVectorMovementNormalized();
		Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

		if(moveDirection != Vector3.zero)
		{
			//because when we dont move, moveDirection == 0, so we cant interact with anything,
			// lastMovementDirection -- remebers where we are looking at(last direction)
			lastMovementDirection = moveDirection; 
		}

		float interactionDistance = 2f;
		if (Physics.Raycast(transform.position, 
							lastMovementDirection, 
							out RaycastHit hit, 
							interactionDistance,countersLayerMask))
		{
			if(hit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
			{
				if(selectedCounter != clearCounter)
				{
					// notify all the listeners that we have new selectedCounter
					SetSelectedCounter(clearCounter);
				}
			}
			else
			{
				// Set selected counter to null
				SetSelectedCounter(null);
			}
		}
		else
		{
			SetSelectedCounter(null);
		}

	}

	public bool IsWalking()
	{
		return isWalking;
	}


	// here we set selected counter
	private void SetSelectedCounter(ClearCounter selectedCounter)
	{
		this.selectedCounter = selectedCounter;
		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
	}

	public Transform GetKicthenObjectFollowTransform() // get kithenObject`s parent
	{
		return kitchenObjectHoldPoint;
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
