using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
	[SerializeField] private GameObject[] visualGameObjectsArray; // we highlight that, when it is selected
	[SerializeField] private BaseCounter baseCounter; // we compare that(e.selectedCounter == clearCounter) so that it knows,if it apllies to it 

	private void Start()
    {
		// subcsribe to event, which is fired(from Player script),when we have new selected counter
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

	private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
	{
		if (e.baseCounter == baseCounter) // if that's us, then highlight that counter
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void Show() // highlight current object
	{
		foreach (var visualItem in visualGameObjectsArray)
		{
			visualItem.SetActive(true);
		}
	}

	private void Hide() // stop highlighting current object
	{
		foreach (var visualItem in visualGameObjectsArray)
		{
			visualItem.SetActive(false);
		}
	}

}
