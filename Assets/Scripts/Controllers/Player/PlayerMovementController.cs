using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isNotStarted = true;
        private bool _isPlayerDead = false;
        private bool _isRopeReached = false;
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
            _data = _manager.GetData();
        }


        private void FixedUpdate()
        {
            if (_isNotStarted || _isPlayerDead)
            {
                return;
            }

            if (_isClicked && _isRopeReached)
            {
                _rig.velocity = new Vector3(0, Mathf.SmoothDamp(_rig.velocity.y, maxStrength, ref a , recoveryRate * Time.fixedDeltaTime), _data.Speed + _data.IncreasedSpeed);
            }
            else
            {
                _rig.velocity = new Vector3(0, _rig.velocity.y, _data.Speed);
            }
        }

        private IEnumerator Later()
        {
            yield return new WaitForSeconds(0.1f);
            _isRopeReached = true;
        }

        public void OnClicked(bool isClicked)
        {
            AudioSignals.Instance.onPlaySound?.Invoke(SoundEnums.Rope);
            _isClicked = isClicked;
            StartCoroutine(Later());
        }
        public void OnReleased(bool isClicked)
        {
            _isClicked = isClicked;
            _isRopeReached = false;
            StopAllCoroutines();
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
        public void OnPlayerDie()
        {
            _isPlayerDead = true;
            _rig.useGravity = false;
            _rig.velocity = Vector3.zero;
        }
        public void OnReset()
        {
            _isNotStarted = true;
            _rig.useGravity = false;
            _rig.velocity = Vector3.zero;
            _isPlayerDead = false;
        }
    }
}