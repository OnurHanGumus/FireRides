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
        [SerializeField] private BoxCollider wallDeactivatorCollider;
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
        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onClicked += _movementController.OnClicked;
            InputSignals.Instance.onInputReleased += _movementController.OnReleased;

            PlayerSignals.Instance.onGetPlayer += OnGetPlayer;

            CoreGameSignals.Instance.onPlay += _movementController.OnPlay;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += _movementController.OnPlayerDie;
            CoreGameSignals.Instance.onRestartLevel += _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;

        }

        private void UnsubscribeEvents()
        {

            InputSignals.Instance.onClicked -= _movementController.OnClicked;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            InputSignals.Instance.onInputReleased -= _movementController.OnReleased;


            PlayerSignals.Instance.onGetPlayer -= OnGetPlayer;

            CoreGameSignals.Instance.onPlay -= _movementController.OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= _movementController.OnPlayerDie;
            CoreGameSignals.Instance.onRestartLevel -= _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;

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

        private void OnPlay()
        {
            wallDeactivatorCollider.enabled = true;

        }

        private void OnResetLevel()
        {
            wallDeactivatorCollider.enabled = false;
            transform.position = new Vector3(0, 0, 10);
        }

    }
}