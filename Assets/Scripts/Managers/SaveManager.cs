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
            //SendPlayerUpgradesInformation();
            //SendGunLevelsInformation();
            //SendSelectedGunIdInformation();
            //SendWorkerUpgradesInformation();
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
            //SaveSignals.Instance.onGetSelectedGun += OnGetSelectedGunId; //Silah veya playere ba�l� objenin idsi

            //LevelSignals.Instance.onBuyArea += OnSaveListAddElement; //A��labilir k�s�mlar varsa bunlar id numaralar� liste i�inde tutulur
            //LevelSignals.Instance.onBuyEnemyArea += OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurret += OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurretOwners += OnSaveListAddElement;

            //LevelSignals.Instance.onMinerCountIncreased += OnIncreaseMinerCount; //oyunda toplay�c� varsa
            //LevelSignals.Instance.onGetMinerCount += OnGetMinerCount;
            //LevelSignals.Instance.onSoldierCountIncreased += OnSaveData;
            //LevelSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            CoreGameSignals.Instance.onSaveAndResetGameData += OnSaveGameData; //Level ge�i�inde temp savelerin temizlenmesi i�indir.
            //PlayerSignals.Instance.onPlayerLeaveBuyArea += SetNewSaveAreaValue; //Idle oyunlarda kay�t i�in
            //PlayerSignals.Instance.onPlayerSelectGun += OnSaveData;
            

            //SaveSignals.Instance.onGetOpenedTurrets += OnGetOpenedTurrets;

            SaveSignals.Instance.onGetBossHealth += OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage += OnSaveData;

            //UISignals.Instance.onChangeGunLevels += OnSaveList; //Sat�n alma yerleri i�in
            //UISignals.Instance.onGetGunLevels += OnGetGunLevels;
            //SaveSignals.Instance.onUpgradePlayer += OnSaveList; 
            //SaveSignals.Instance.onUpgradeWorker += OnSaveList;
            //SaveSignals.Instance.onGetWorkerUpgrades += OnGetWorkerUpgrades;
            LevelSignals.Instance.onGetAreasCount += OnGetAreaCounts;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveCollectables -= OnSaveData;
            SaveSignals.Instance.onSaveScore -= OnSaveData;
            SaveSignals.Instance.onChangeSoundState -= OnSaveData;

            SaveSignals.Instance.onGetSoundState -= OnGetData;

            //SaveSignals.Instance.onGetSelectedGun -= OnGetSelectedGunId;


            //LevelSignals.Instance.onBuyArea -= OnSaveListAddElement;
            //LevelSignals.Instance.onBuyEnemyArea -= OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurret -= OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurretOwners -= OnSaveListAddElement;

            //LevelSignals.Instance.onMinerCountIncreased -= OnIncreaseMinerCount;
            //LevelSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            //LevelSignals.Instance.onSoldierCountIncreased -= OnSaveData;
            //LevelSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            CoreGameSignals.Instance.onSaveAndResetGameData -= OnSaveGameData;
            //PlayerSignals.Instance.onPlayerLeaveBuyArea -= SetNewSaveAreaValue;
            //PlayerSignals.Instance.onPlayerSelectGun -= OnSaveData;


            //SaveSignals.Instance.onGetOpenedTurrets -= OnGetOpenedTurrets;
            SaveSignals.Instance.onGetBossHealth -= OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage -= OnSaveData;
            //UISignals.Instance.onChangeGunLevels -= OnSaveList;
            //UISignals.Instance.onGetGunLevels -= OnGetGunLevels;
            //SaveSignals.Instance.onUpgradePlayer -= OnSaveList;
            //SaveSignals.Instance.onUpgradeWorker -= OnSaveList;
            //SaveSignals.Instance.onGetWorkerUpgrades -= OnGetWorkerUpgrades;
            LevelSignals.Instance.onGetAreasCount -= OnGetAreaCounts;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSaveListAddElement(int id, SaveLoadStates saveType)
        {
            _saveGameCommand.OnSaveListAddElement(saveType, id);
        }

        private void OnSaveList(List<int> listToSave, SaveLoadStates saveType, SaveFiles fileName)
        {
            _saveGameCommand.OnSaveList(saveType, listToSave, fileName.ToString());
        }

        private void OnSaveData(int value, SaveLoadStates saveType, SaveFiles saveFiles)
        {
            _saveGameCommand.OnSaveData(saveType, value, saveFiles.ToString());

        }
        private void OnIncreaseMinerCount(int increaseAmount) //bu kullan�m da vard�r ancak daha g�zeli direk de�eri g�ndermektir.
        {
            int currentCount = _loadGameCommand.OnLoadGameData(SaveLoadStates.MinerCount, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(SaveLoadStates.MinerCount, currentCount + increaseAmount, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void OnSaveGameData() //level ge�i�lerindeki kay�t i�lemidir. Runnderse gold ve gem burada kaydedilir ancak idle oyunsa burada kay�t atmaya gerek yok ��nk� topland��� an kaydedilir. Ge�ici de�erler s�f�rlan�r.
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
        
        private List<int> OnGetGunLevels()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString());
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