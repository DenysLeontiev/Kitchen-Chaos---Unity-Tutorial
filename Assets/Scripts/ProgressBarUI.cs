using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private GameObject hasProgressGameObject; // field to get interface, because by default we cant do that in unity
    [SerializeField] private Image barImage;

    private IHasProgress counter;

    private void Start()
    {
		counter = hasProgressGameObject.GetComponent<IHasProgress>();
		counter.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;

		Hide();
    }

	private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
	{
        barImage.fillAmount = e.cuttingProgressNormalized;

		if (e.cuttingProgressNormalized == 0f || e.cuttingProgressNormalized == 1f)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}
	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}

}
