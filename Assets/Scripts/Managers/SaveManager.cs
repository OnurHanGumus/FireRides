using System;
using System.Collections.Generic;
using Commands;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables
        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();

            SendCollectablesInformation();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveCollectables += OnSaveData;
            SaveSignals.Instance.onSaveScore += OnSaveData;
            SaveSignals.Instance.onChangeSoundState += OnSaveData;
            SaveSignals.Instance.onGetScore += OnGetData;
            SaveSignals.Instance.onGetSoundState += OnGetData;
            CoreGameSignals.Instance.onSaveAndResetGameData += OnSaveGameData; //Level geçiþinde temp savelerin temizlenmesi içindir.
            SaveSignals.Instance.onGetBossHealth += OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage += OnSaveData;
            LevelSignals.Instance.onGetAreasCount += OnGetAreaCounts;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveCollectables -= OnSaveData;
            SaveSignals.Instance.onSaveScore -= OnSaveData;
            SaveSignals.Instance.onChangeSoundState -= OnSaveData;
            SaveSignals.Instance.onGetSoundState -= OnGetData;
            CoreGameSignals.Instance.onSaveAndResetGameData -= OnSaveGameData;
            SaveSignals.Instance.onGetBossHealth -= OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage -= OnSaveData;
            LevelSignals.Instance.onGetAreasCount -= OnGetAreaCounts;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSaveData(int value, SaveLoadStates saveType, SaveFiles saveFiles)
        {
            _saveGameCommand.OnSaveData(saveType, value, saveFiles.ToString());

        }

        private void OnSaveGameData() //level geçiþlerindeki kayýt iþlemidir. Runnderse gold ve gem burada kaydedilir ancak idle oyunsa burada kayýt atmaya gerek yok çünkü toplandýðý an kaydedilir. Geçici deðerler sýfýrlanýr.
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.Level, _loadGameCommand.OnLoadGameData(SaveLoadStates.Level) + 1);
            _saveGameCommand.OnSaveData(SaveLoadStates.Money, _loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            _saveGameCommand.OnSaveData(SaveLoadStates.Gem, _loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }
        private void SendCollectablesInformation() //Essential
        {
            SaveSignals.Instance.onInitializeSetMoney?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            SaveSignals.Instance.onInitializeSetGem?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }

        private int OnGetData(SaveLoadStates state, SaveFiles file)
        {
            return _loadGameCommand.OnLoadGameData(state, file.ToString());

        }
        private int[] OnGetAreaCounts(SaveLoadStates saveType)
        {
            return _loadGameCommand.OnLoadArray(saveType, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private int OnGetBossHealth()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.BossHealth, SaveFiles.SaveFile.ToString());
        }
    }
}