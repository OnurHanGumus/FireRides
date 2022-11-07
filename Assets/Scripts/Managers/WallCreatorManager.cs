using System;
using System.Collections;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Managers
{
    public class WallCreatorManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private int colorTurn = 0;
        [SerializeField] private int lastWallZPos = 98;
        #endregion

        #region Private Variables
        private bool _isReset = false;
        #endregion

        #endregion




        #region Event Subscription

        private void Awake()
        {
            //StartCoroutine(MoveForward());
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncreased;
            CoreGameSignals.Instance.onRestartLevel += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }



        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncreased;
            CoreGameSignals.Instance.onPlay -= OnPlay;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncreased(ScoreTypeEnums type, int value)
        {
            if (_isReset)
            {
                return;
            }
            GameObject wall;
            if (colorTurn == 0)
            {
                wall = PoolSignals.Instance.onGetLightWallFromPool();
                colorTurn++;

            }
            else
            {
                wall = PoolSignals.Instance.onGetDarkWallFromPool();
                colorTurn = 0;
            }
            if (wall == null)
            {
                Debug.Log("nullmuþ");
                return;
            }
            lastWallZPos += 2;
            wall.transform.position = new Vector3(0, UnityEngine.Random.Range(-4, 5), lastWallZPos);
            wall.SetActive(true);
            Debug.Log("taþýndý");

        }

        private void OnPlay()
        {
            _isReset = false;
        }
        private void OnReset()
        {
            _isReset = true;
            lastWallZPos = 98;
        }
    }
}