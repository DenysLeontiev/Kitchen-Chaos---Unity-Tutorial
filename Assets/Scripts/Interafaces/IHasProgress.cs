using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
	// we listen to that, when we have cut something and also we send our current cutting progress
	public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
	public class OnProgressChangedEventArgs
	{
		public float cuttingProgressNormalized;
	}
}
