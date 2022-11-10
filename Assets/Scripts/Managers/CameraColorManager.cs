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
    public class CameraColorManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField] private Color[] colorArray;
        [SerializeField] private Color currentColor;

        #endregion

        #region Private Variables
        private Camera _cam;
        private int _levelId = 0;
        private int _totalLevelCount = 0;
        #endregion

        #endregion

        #region Event Subscription

        private void Start()
        {
            Init();
            ChangeColor();
        }

        private void Init()
        {
            _cam = GetComponent<Camera>();
            _totalLevelCount = LevelSignals.Instance.onGetTotalLevelCount();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel += OnLevelSuccessFul;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }



        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel -= OnLevelSuccessFul;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnLevelSuccessFul()
        {
            if (_totalLevelCount - 1 == _levelId)
            {
                _levelId = 0;
            }
            else
            {
                ++_levelId;
            }
            ChangeColor();
        }
        private void ChangeColor()
        {
            _cam.backgroundColor = colorArray[_levelId];
            RenderSettings.fogColor = colorArray[_levelId];
        }

        private void OnRestartLevel()
        {
            _levelId = 0;
            ChangeColor();
        }
    }
}