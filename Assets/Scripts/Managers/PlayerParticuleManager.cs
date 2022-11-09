using Controllers;
using Enums;
using Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerParticuleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private List<ParticleSystem> particules;


        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlay()
        {
            foreach (var i in particules)
            {
                i.Clear();
            }
        }
    }
}