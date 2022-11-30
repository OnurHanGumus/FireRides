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
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveScore += OnSaveData;
            SaveSignals.Instance.onSaveMoney += OnSaveData;
            SaveSignals.Instance.onChangeSoundState += OnSaveData;
            SaveSignals.Instance.onChangeOpenedItems += OnSaveList;
            SaveSignals.Instance.onSelectTarget += OnSaveData;
            SaveSignals.Instance.onGetScore += OnGetData;
            SaveSignals.Instance.onGetSoundState += OnGetData;
            SaveSignals.Instance.onGetSelectedTargetId += OnGetData;
            SaveSignals.Instance.onGetOpenedItems += OnGetListData;
            SaveSignals.Instance.onSaveMoney += OnSaveData;
            SaveSignals.Instance.onGetMoney += OnGetData;

        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveScore -= OnSaveData;
            SaveSignals.Instance.onSaveMoney -= OnSaveData;
            SaveSignals.Instance.onChangeSoundState -= OnSaveData;
            SaveSignals.Instance.onChangeOpenedItems -= OnSaveList;
            SaveSignals.Instance.onSelectTarget -= OnSaveData;
            SaveSignals.Instance.onGetScore -= OnGetData;
            SaveSignals.Instance.onGetSoundState -= OnGetData;
            SaveSignals.Instance.onGetSelectedTargetId -= OnGetData;
            SaveSignals.Instance.onGetOpenedItems -= OnGetListData;
            SaveSignals.Instance.onGetMoney -= OnGetData;

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
        private void OnSaveList(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.BuyedItemList, listToSave, SaveFiles.SaveFile.ToString());
        }
        private int OnGetData(SaveLoadStates state, SaveFiles file)
        {
            return _loadGameCommand.OnLoadGameData(state, file.ToString());
        }
        private List<int> OnGetListData(SaveLoadStates state, SaveFiles file)
        {
            return _loadGameCommand.OnLoadList(state, file.ToString());
        }
        
    }
}