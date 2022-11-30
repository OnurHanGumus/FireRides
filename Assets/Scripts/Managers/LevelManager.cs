using System;
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
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;


        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        private int _totalLevelCount = 0;
        private UnityEngine.Object[] _levels;
        private WallData _data;
        private int _poolObjectCount;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _levels = Resources.LoadAll("Levels");
            _totalLevelCount = _levels.Length;
            _data = GetData();

        }
        private WallData GetData() => Resources.Load<CD_Wall>("Data/CD_Wall").wallData;


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onLevelFailed += OnRestart;
            CoreGameSignals.Instance.onPlay += OnPlay;

            LevelSignals.Instance.onGetTotalLevelCount += OnGetTotalLevelCount;
            PoolSignals.Instance.onInitializeAmountOfPool += OnInitializePools;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
            CoreGameSignals.Instance.onLevelFailed -= OnRestart;

            LevelSignals.Instance.onGetTotalLevelCount -= OnGetTotalLevelCount;
            PoolSignals.Instance.onInitializeAmountOfPool -= OnInitializePools;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
        }

        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();

        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            _levelID = 0;

        }

        private int OnGetLevelID()
        {
            return _levelID;
        }


        private void OnInitializeLevel()
        {
            int newLevelId = _levelID % _levels.Length;
            levelLoader.InitializeLevel((GameObject)_levels[newLevelId], levelHolder.transform, _levelID, _data, _poolObjectCount);
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnRestart()
        {
        }

        private int OnGetTotalLevelCount()
        {
            return _totalLevelCount;
        }

        private void OnInitializePools(int amount)
        {
            _poolObjectCount = amount;
        }
        private void OnPlay()
        {
            OnInitializeLevel();
        }
    }
}