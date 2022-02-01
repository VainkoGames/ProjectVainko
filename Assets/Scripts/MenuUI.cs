using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    private void Start()
    {
        
    }

    
    private void Update()
    {
        UpdateCoinField(0, 0, 0);
    }

    public void UpdateCoinField(int inInitialCoinValue , int inUpdatedCoinValue , int inStage)
    {
        if (inStage == 1)
        {
            coinText.DOCounter(inInitialCoinValue, inUpdatedCoinValue, 1f, true, System.Globalization.CultureInfo.InvariantCulture)
                .OnComplete(() => 
                { 
                    coinText.text = inUpdatedCoinValue.ToString();
                    //coinText.text.
                });
        }
        else
        {
            string coinValue = GameManager.Instance._coins.ToString("N1", System.Globalization.CultureInfo.InvariantCulture);
            coinValue = coinValue.Substring(0, coinValue.Length - 2);
            coinText.text = coinValue;
        }
    }
}
