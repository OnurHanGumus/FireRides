using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables
        private int _score = 0;
        private int _money = 0;

        [ShowInInspector]
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        [ShowInInspector]

        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {

        }
        private void Start()
        {
            InitializeMoney();
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.Instance.onMoneyIncreased += OnMoneyIncrease;
            ScoreSignals.Instance.onMoneyDecrease += OnMoneyDecrease;
            ScoreSignals.Instance.onGetScore += OnGetScore;
            ScoreSignals.Instance.onGetMoney += OnGetMoney;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel += OnReset;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.Instance.onMoneyIncreased -= OnMoneyIncrease;
            ScoreSignals.Instance.onMoneyDecrease -= OnMoneyDecrease;
            ScoreSignals.Instance.onGetScore -= OnGetScore;
            ScoreSignals.Instance.onGetMoney -= OnGetMoney;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            Score += amount;
            UISignals.Instance.onSetChangedText?.Invoke(type, Score);

        }

        private void OnMoneyIncrease(ScoreTypeEnums type, int amount)
        {
            Money += amount;
            UISignals.Instance.onSetChangedText?.Invoke(type, Money);
            SaveSignals.Instance.onSaveMoney?.Invoke(Money, SaveLoadStates.Money, SaveFiles.SaveFile);
        }

        private void OnMoneyDecrease(ScoreTypeEnums type, int amount)
        {
            if (type.Equals(ScoreTypeEnums.Money))
            {
                Money -= amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, Money);
            }
        }

        private int OnGetScore()
        {
            return Score;
        }
        private int OnGetMoney()
        {
            return Money;
        }

        private void InitializeMoney()
        {
            Money = SaveSignals.Instance.onGetMoney(SaveLoadStates.Money, SaveFiles.SaveFile);
            UISignals.Instance.onSetChangedText?.Invoke(ScoreTypeEnums.Money, Money);

        }
        private void OnPlay()
        {
            Score = 0;
            UISignals.Instance.onSetChangedText?.Invoke(ScoreTypeEnums.Score, Score);

        }
        private void OnReset()
        {
            Score = 0;
            UISignals.Instance.onSetChangedText?.Invoke(ScoreTypeEnums.Score, Score);
        }
        private void OnLevelFailed()
        {
            ScoreSignals.Instance.onMoneyIncreased.Invoke(ScoreTypeEnums.Money, Score); //money her el score kadar artsýn
        }
    }
}