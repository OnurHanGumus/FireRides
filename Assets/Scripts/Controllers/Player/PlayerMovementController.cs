using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private float speed = 2;
        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private float _xValue = 2, _zValue;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isNotStarted = true;
        private float a = 0;

        float maxStrength = 15;
        float recoveryRate = 25f;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager>();
            _data = _manager.GetPlayerData();
        }


        private void FixedUpdate()
        {
            if (_isNotStarted)
            {
                return;
            }

            if (_isClicked)
            {
                _rig.velocity = new Vector3(0, Mathf.SmoothDamp(_rig.velocity.y, maxStrength, ref a , recoveryRate * Time.fixedDeltaTime), speed + 5);
            }
            else
            {
                _rig.velocity = new Vector3(0, _rig.velocity.y, speed);
            }
        }

        public void OnClicked(bool isClicked)
        {
            _isClicked = isClicked;
        }
        public void OnReleased(bool isClicked)
        {
            _isClicked = isClicked;
        }

        public float OnGetPlayerSpeed()
        {
            return _data.Speed;
        }

        public void OnPlay()
        {
            _isNotStarted = false;
            _rig.useGravity = true;
        }
        public void OnReset()
        {
            _isNotStarted = true;
            _rig.useGravity = false;
            _rig.velocity = Vector3.zero;
        }
    }
}