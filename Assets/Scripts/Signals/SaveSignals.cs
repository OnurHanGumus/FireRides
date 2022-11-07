using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class SaveSignals: MonoSingleton<SaveSignals>
    {
        public UnityAction<int,SaveLoadStates,SaveFiles> onSaveScore = delegate { };


        public UnityAction<List<int>> onInitializeTurretOwners = delegate { };
        public UnityAction<List<int>> onInitializeEnemyAreas = delegate { };

        public UnityAction<int> onInitializeSetMoney = delegate { };
        public UnityAction<int> onInitializeSetGem = delegate { };

        public UnityAction<int> onInitializePlayerCapacity = delegate { };
        public UnityAction<int> onInitializePlayerSpeed = delegate { };
        public UnityAction<int> onInitializePlayerHealth = delegate { };

        public UnityAction<List<int>,SaveLoadStates,SaveFiles> onInitializePlayerUpgrades = delegate { };
        public UnityAction<int,SaveLoadStates, SaveFiles> onInitializeSelectedGunId = delegate { };

        public UnityAction<List<int>> onInitializeWorkerUpgrades = delegate { };


        public UnityAction<int,SaveLoadStates,SaveFiles> onSaveCollectables = delegate { };

        public Func<int> onGetSelectedGun = delegate { return 0; };

        public UnityAction<List<int>,SaveLoadStates,SaveFiles> onUpgradePlayer = delegate { };
        public UnityAction<List<int>, SaveLoadStates, SaveFiles> onUpgradeWorker = delegate { };

        public UnityAction<List<int>> onInitializeOpenedTurretInfo = delegate { };

        public Func<List<int>> onGetOpenedTurrets = delegate { return null; };
        public Func<List<int>> onGetWorkerUpgrades = delegate { return null; };

        //------------------------------------------------------------
        public UnityAction<SaveLoadStates> onIncreaseMoneyWorkerCount = delegate { };
        public UnityAction<SaveLoadStates> onIncreaseAmmoWorkerCount = delegate { };

        public UnityAction<int> onInitializeMoneyWorkerCount = delegate { };
        public UnityAction<int> onInitializeAmmoWorkerCount = delegate { };

        public Func<int> onGetBossHealth = delegate { return 0; };
        public UnityAction<int,SaveLoadStates,SaveFiles> onBossTakedDamage = delegate { };

        public Func<SaveLoadStates,SaveFiles,int> onGetScore = delegate { return 0; };
        public Func<SaveLoadStates,SaveFiles,int> onGetSoundState = delegate { return 0; };
        public UnityAction<int, SaveLoadStates, SaveFiles> onChangeSoundState = delegate { };




    }
}