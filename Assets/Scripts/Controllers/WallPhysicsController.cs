using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;
using Data.UnityObject;

namespace Controllers
{
    public class WallPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private WallManager manager;

        #endregion
        #region Private Variables
        //private EnemyData _data;

        #endregion
        #endregion

        private void Start()
        {
            //_data = manager.GetEnemyData();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, 1);
            }
        }
    }
}