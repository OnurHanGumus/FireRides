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
        private WallData _data;
        private int _defaultStartPos = 0;
        private int _levelId = 0;
        #endregion

        #endregion

        private WallData GetData() => Resources.Load<CD_Wall>("Data/CD_Wall").wallData;


        #region Event Subscription

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
            lastWallZPos = (((PoolSignals.Instance.onGetAmount()-1)*2+1)*2);
            _defaultStartPos = lastWallZPos;
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
            CoreGameSignals.Instance.onNextLevel += OnLevelIncreased;
        }



        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncreased;
            CoreGameSignals.Instance.onRestartLevel += OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnLevelIncreased;

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

            wall.transform.position = new Vector3(0, UnityEngine.Random.Range(_data.Y_MinRandomPos, _data.Y_MaxRandomPos), lastWallZPos);
            wall.SetActive(true);
            Debug.Log("taþýndý");

        }

        private void OnPlay()
        {
            _isReset = false;
        }
        private void OnLevelIncreased()
        {
            ++_levelId;
        }
        private void OnReset()
        {
            _isReset = true;
            _levelId = 0;
            lastWallZPos = _defaultStartPos;
        }
    }
}