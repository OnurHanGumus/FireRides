using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
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
            }
            else if (other.CompareTag("NextLevel"))
            {
                CoreGameSignals.Instance.onNextLevel?.Invoke();
            }
        }

        public void OnPlay()
        {

        }
        public void OnReset()
        {

        }
    }
}