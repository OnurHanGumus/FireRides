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
            //SaveSignals.Instance.onGetSelectedGun += OnGetSelectedGunId; //Silah veya playere baðlý objenin idsi

            //LevelSignals.Instance.onBuyArea += OnSaveListAddElement; //Açýlabilir kýsýmlar varsa bunlar id numaralarý liste içinde tutulur
            //LevelSignals.Instance.onBuyEnemyArea += OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurret += OnSaveListAddElement;
            //LevelSignals.Instance.onBuyTurretOwners += OnSaveListAddElement;

            //LevelSignals.Instance.onMinerCountIncreased += OnIncreaseMinerCount; //oyunda toplayýcý varsa
            //LevelSignals.Instance.onGetMinerCount += OnGetMinerCount;
            //LevelSignals.Instance.onSoldierCountIncreased += OnSaveData;
            //LevelSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            CoreGameSignals.Instance.onSaveAndResetGameData += OnSaveGameData; //Level geçiþinde temp savelerin temizlenmesi içindir.
            //PlayerSignals.Instance.onPlayerLeaveBuyArea += SetNewSaveAreaValue; //Idle oyunlarda kayýt için
            //PlayerSignals.Instance.onPlayerSelectGun += OnSaveData;
            

            //SaveSignals.Instance.onGetOpenedTurrets += OnGetOpenedTurrets;

            SaveSignals.Instance.onGetBossHealth += OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage += OnSaveData;

            //UISignals.Instance.onChangeGunLevels += OnSaveList; //Satýn alma yerleri için
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
        private void OnIncreaseMinerCount(int increaseAmount) //bu kullaným da vardýr ancak daha güzeli direk deðeri göndermektir.
        {
            int currentCount = _loadGameCommand.OnLoadGameData(SaveLoadStates.MinerCount, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(SaveLoadStates.MinerCount, currentCount + increaseAmount, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void OnSaveGameData() //level geçiþlerindeki kayýt iþlemidir. Runnderse gold ve gem burada kaydedilir ancak idle oyunsa burada kayýt atmaya gerek yok çünkü toplandýðý an kaydedilir. Geçici deðerler sýfýrlanýr.
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.Level, _loadGameCommand.OnLoadGameData(SaveLoadStates.Level) + 1);
            _saveGameCommand.OnSaveData(SaveLoadStates.Money, _loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            _saveGameCommand.OnSaveData(SaveLoadStates.Gem, _loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));

            //_saveGameCommand.OnResetList(SaveLoadStates.CurrentLevelOpenedAreas);
            //_saveGameCommand.OnResetList(SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnResetList(SaveLoadStates.OpenedTurretOwners, SaveFiles.WorkerCurrentCounts.ToString());

            //_saveGameCommand.OnResetArray(SaveLoadStates.OpenedAreasCounts, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnResetArray(SaveLoadStates.OpenedTurretsCounts, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnResetArray(SaveLoadStates.OpenedTurretOwnersCounts, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnResetArray(SaveLoadStates.OpenedEnemyAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());

            //_saveGameCommand.OnResetArray(SaveLoadStates.AmmoWorkerAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnResetArray(SaveLoadStates.MoneyWorkerAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());

            //_saveGameCommand.OnSaveData(SaveLoadStates.MinerCount, 0, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnSaveData(SaveLoadStates.SoldierCount, 0, SaveFiles.WorkerCurrentCounts.ToString());
            //_saveGameCommand.OnSaveData(SaveLoadStates.BossHealth, 0, SaveFiles.SaveFile.ToString());
        }

        //private void SetNewSaveAreaValue(SaveLoadStates type, int[] newArray) //Areadan çýkýnca mevcut satýn alma durumunu kaydeder
        //{
        //    _saveGameCommand.OnSaveArray(type, newArray, SaveFiles.WorkerCurrentCounts.ToString());
        //}
        private void SendCollectablesInformation() //Essential
        {
            SaveSignals.Instance.onInitializeSetMoney?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            SaveSignals.Instance.onInitializeSetGem?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }
        //private void SendPlayerUpgradesInformation()
        //{
        //    List<int> temp = _loadGameCommand.OnLoadList(SaveLoadStates.PlayerUpgrades, SaveFiles.PlayerImprovements.ToString());
        //    SaveSignals.Instance.onInitializePlayerUpgrades?.Invoke(temp, SaveLoadStates.PlayerUpgrades, SaveFiles.PlayerImprovements);
        //}
        //private void SendWorkerUpgradesInformation()
        //{
        //    List<int> temp = _loadGameCommand.OnLoadList(SaveLoadStates.WorkerUpgrades, SaveFiles.WorkerUpgrades.ToString());

        //    SaveSignals.Instance.onInitializeWorkerUpgrades?.Invoke(temp);
        //}
        //private void SendGunLevelsInformation()
        //{
        //    UISignals.Instance.onInitializeGunLevels?.Invoke(_loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString()),SaveLoadStates.GunLevels, SaveFiles.Guns);
        //}

        //private void SendSelectedGunIdInformation()
        //{
        //    SaveSignals.Instance.onInitializeSelectedGunId?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.GunId, SaveFiles.Guns.ToString()),SaveLoadStates.GunId, SaveFiles.Guns);
        //}
        private int OnGetData(SaveLoadStates state, SaveFiles file)
        {
            return _loadGameCommand.OnLoadGameData(state, file.ToString());

        }
        
        private List<int> OnGetGunLevels()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString());
        }
        private List<int> OnGetOpenedTurrets()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString());

        }
        private List<int> OnGetWorkerUpgrades()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.WorkerUpgrades, SaveFiles.WorkerUpgrades.ToString());

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