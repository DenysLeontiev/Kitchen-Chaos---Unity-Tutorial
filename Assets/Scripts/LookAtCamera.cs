using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode 
    {
        LookAt,
        LookAtInverted,
        Forward,
        ForwardInverted,
	}

    [SerializeField] private Mode mode;


	void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + directionFromCamera);
                break;
            case Mode.Forward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.ForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
