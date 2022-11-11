using System;
using System.Collections;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Extentions;
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
        private bool _isReset = false;
        private WallData _data;
        private int _defaultStartPos = 0;
        private int _lastWallZPos = 0;
        private int _poolWallCount;

        #endregion

        #endregion

        private WallData GetData() => Resources.Load<CD_Wall>("Data/CD_Wall").wallData;


        #region Event Subscription

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
            _lastWallZPos = _data.WallZAxisLenght * _poolWallCount - (_data.WallZAxisLenght * 2);
            _defaultStartPos = _lastWallZPos;
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

            PoolSignals.Instance.onInitializeAmountOfPool += OnInitializeAmountPool;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncreased;
            CoreGameSignals.Instance.onRestartLevel += OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            PoolSignals.Instance.onInitializeAmountOfPool -= OnInitializeAmountPool;
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
                return;
            }
            _lastWallZPos += _data.WallZAxisLenght;

            wall.transform.position = new Vector3(0, UnityEngine.Random.Range(_data.Y_MinRandomPos, _data.Y_MaxRandomPos), _lastWallZPos + _data.WallZAxisLenght);
            wall.SetActive(true);
        }

        private void OnPlay()
        {
            _isReset = false;
        }
        private void OnReset()
        {
            _isReset = true;
            _lastWallZPos = _defaultStartPos;
        }
        private void OnInitializeAmountPool(int amount)
        {
            _poolWallCount = amount;
            Debug.Log(amount);
        }
    }
}