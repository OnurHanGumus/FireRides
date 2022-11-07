using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;


namespace Managers
{

    public class WallManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables
        [SerializeField] private int maksDistance = 10;
        #endregion

        #region Private Variables
        private Transform _player;
        #endregion

        #endregion




        #region Event Subscription


        private void Init()
        {
            _player = PlayerSignals.Instance.onGetPlayer();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            Init();

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
            if (_player == null)
            {
                Debug.Log("hala null");
                return;
            }
            if (_player.transform.position.z - transform.position.z > maksDistance)
            {
                gameObject.SetActive(false);
            }
        }


    }
}