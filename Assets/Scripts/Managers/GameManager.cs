using System;
using Enums;
using Extentions;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public GameStates States;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    #region Event Subcription
    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
    }

    private void UnsubscribeEvents()
    {
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
}