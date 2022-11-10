using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private PlayerManager manager;
        #endregion

        #region Private Variables


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Grappable") || other.CompareTag("Wall"))
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                AudioSignals.Instance.onPlaySound?.Invoke(SoundEnums.Sonme);

            }
            else if (other.CompareTag("NextLevel"))
            {
                CoreGameSignals.Instance.onNextLevel?.Invoke();
                ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, 10);
                ScoreSignals.Instance.onHitTarget?.Invoke(10);
                AudioSignals.Instance.onPlaySound?.Invoke(SoundEnums.Breake);

            }
            else if (other.CompareTag("TargetMiddle"))
            {
                ScoreSignals.Instance.onHitTarget?.Invoke(2);
                AudioSignals.Instance.onPlaySound?.Invoke(SoundEnums.Breake);

            }
            else if (other.CompareTag("TargetBig"))
            {
                ScoreSignals.Instance.onHitTarget?.Invoke(1);
                AudioSignals.Instance.onPlaySound?.Invoke(SoundEnums.Breake);
            }
        }
    }
}