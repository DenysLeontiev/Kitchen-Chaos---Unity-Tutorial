using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        barImage.fillAmount = 0f;

		Hide();
    }

	private void Update()
	{
		
	}

	private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
