using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI scoreTxt;
    #endregion
    #endregion
    private void Start()
    {

    }

    public void OnScoreUpdateText(ScoreTypeEnums type,int score)
    {
        scoreTxt.text = score.ToString();
    }
}
