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
        #endregion

        #region Private Variables

        #endregion

        #endregion




        #region Event Subscription

        private void Awake()
        {
            StartCoroutine(MoveForward());
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncreased;
        }



        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncreased;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private IEnumerator MoveForward()
        {
            yield return new WaitForSeconds(1f);
            OnScoreIncreased(ScoreTypeEnums.Score, 2);
            StartCoroutine(MoveForward());
        }

        private void OnScoreIncreased(ScoreTypeEnums type, int value)
        {
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
                return;
            }
            wall.SetActive(true);
            wall.transform.position = new Vector3(0, UnityEngine.Random.Range(-4, 5), value * 2);
            Debug.Log("�telendi");
        }
    }
}