using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsPlayerDead = false;



        #endregion

        #region Serialized Variables



        #endregion

        #region Private Variables
        private PlayerData _data;
        private PlayerMovementController _movementController;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

            _data = GetData();
            _movementController = GetComponent<PlayerMovementController>();
        }
        private PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onClicked += _movementController.OnClicked;
            InputSignals.Instance.onInputReleased += _movementController.OnReleased;

        }

        private void UnsubscribeEvents()
        {

            InputSignals.Instance.onClicked -= _movementController.OnClicked;
            InputSignals.Instance.onInputReleased -= _movementController.OnReleased;




        }

        public PlayerData GetPlayerData()
        {
            return _data;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private Transform OnGetPlayer()
        {
            return transform;
        }

    }
}