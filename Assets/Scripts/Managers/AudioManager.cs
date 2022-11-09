using Controllers;
using Enums;
using Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private List<AudioSource> sources;
        [SerializeField] private List<AudioClip> sounds;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AudioSignals.Instance.onPlaySound += OnPlaySound;
        }

        private void UnsubscribeEvents()
        {
            AudioSignals.Instance.onPlaySound -= OnPlaySound;


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlaySound(SoundEnums id)
        {
            if (!sources[0].isPlaying)
            {
                sources[0].PlayOneShot(sounds[(int)id]);
            }
            else
            {
                sources[1].PlayOneShot(sounds[(int)id]);
            }
        }

        public void ButtonClickSound()
        {
            OnPlaySound(SoundEnums.Click);
        }
    }
}