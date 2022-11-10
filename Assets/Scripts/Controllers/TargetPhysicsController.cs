using System.Collections.Generic;
using Data.UnityObject;
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
        private TargetData _data;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
            transform.eulerAngles = new Vector3(Random.Range(_data.MinEulerXValue, _data.MaxEulerXValue), _data.PreferedEulerYValue, 0);
        }

        private TargetData GetData() => Resources.Load<CD_Target>("Data/CD_Target").TargetData;

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