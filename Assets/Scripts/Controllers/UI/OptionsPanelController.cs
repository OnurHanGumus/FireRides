using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private Toggle soundToggle;
    #endregion
    #region Private Variables
    private bool _audioDisabled;
    #endregion
    #endregion

    private void Start()
    {
        _audioDisabled = SaveSignals.Instance.onGetSoundState(SaveLoadStates.SoundState, SaveFiles.GameOptions) == 0;
        soundToggle.isOn = !_audioDisabled;
        EnableAudioSource();
    }
    public void OnValueChanged()
    {
        SaveSignals.Instance.onChangeSoundState?.Invoke(soundToggle.isOn ? 1 : 0, SaveLoadStates.SoundState, SaveFiles.GameOptions);
        _audioDisabled = !_audioDisabled;
        EnableAudioSource();
    }
    public void CloseOptionsPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.OptionsPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    private void EnableAudioSource()
    {
        AudioListener.pause = _audioDisabled;
    }
}
