using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class TargetInstantiator : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField] private GameObject prefab;
        #endregion

        #region Private Variables
        private int _targetId = 0;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
            GetSelectedTarget();
            CreateTarget();
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
        }

        private void UnsubscribeEvents()
        {
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetSelectedTarget()
        {
            _targetId = SaveSignals.Instance.onGetSelectedTargetId(SaveLoadStates.TargetId, SaveFiles.SaveFile);
            prefab = (GameObject) Resources.Load("Prefabs/Targets/"+ _targetId.ToString());
            Debug.Log("Prefabs/Targets/" + _targetId.ToString());
        }

        private void CreateTarget()
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}