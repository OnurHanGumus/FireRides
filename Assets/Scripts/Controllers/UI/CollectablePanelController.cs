using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePanelController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI scoreTxt;

    #endregion

    #region Private Variables

    #endregion

    #endregion


    public void OnMoneyIncreased(ScoreTypeEnums type, int value)
    {
        if (type.Equals(ScoreTypeEnums.Money))
        {
            scoreTxt.text = value.ToString();
        }
    }
}
