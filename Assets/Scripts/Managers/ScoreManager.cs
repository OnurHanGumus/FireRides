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

        [ShowInInspector]
        public int Score
        {
            get { return _score; }
            set { _score = value; }
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
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease += OnScoreDecrease;
            ScoreSignals.Instance.onGetScore += OnGetScore;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnReset;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease -= OnScoreDecrease;
            ScoreSignals.Instance.onGetScore -= OnGetScore;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            if (type.Equals(ScoreTypeEnums.Score))
            {
                _score += amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, _score);
            }
        }

        private void OnScoreDecrease(ScoreTypeEnums type, int amount)
        {

        }

        private int OnGetScore()
        {
            return Score;
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
    }
}