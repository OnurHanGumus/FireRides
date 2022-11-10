using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private bool _isPlayerDead = false;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                if (_isPlayerDead)
                {
                    return;
                }

                InputSignals.Instance.onClicked?.Invoke(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                InputSignals.Instance.onInputReleased?.Invoke(false);
            }
        }
        private bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}