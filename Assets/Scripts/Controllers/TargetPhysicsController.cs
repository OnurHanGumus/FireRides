using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class TargetPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
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
            transform.eulerAngles = new Vector3(Random.Range(-16,17), 162,0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("WallDeactivator"))
            {
                ScoreSignals.Instance.onMissTarget?.Invoke();
            }
        }
    }
}