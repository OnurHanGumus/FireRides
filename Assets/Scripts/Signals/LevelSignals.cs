using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction<int,SaveLoadStates> onBuyArea = delegate { };
        public UnityAction<int,SaveLoadStates> onBuyEnemyArea = delegate { };
        public UnityAction<int,SaveLoadStates> onBuyTurret = delegate { };
        public UnityAction<int,SaveLoadStates> onBuyTurretOwners = delegate { };

        public UnityAction<int> onMinerCountIncreased = delegate { };
        public Func<int> onGetMinerCount = delegate { return 0; };
        public Func<int> onGetMineRemainCapacity = delegate { return 0; };

        public UnityAction<int> onMilitaryPopulationIncreased = delegate { };
        public UnityAction<int,SaveLoadStates,SaveFiles> onSoldierCountIncreased = delegate { };
        public Func<int> onGetMilitaryTotalCapacity = delegate { return 0; };
        public Func<int> onGetEmptyReadySoldiersCount = delegate { return 0; };
        public Func<int> onGetSoldierCount = delegate { return 0; };


        public UnityAction onBossDefeated = delegate { };
        public UnityAction onPlayerReachedToNewBase = delegate { };

        public UnityAction onMineGemCapacityFull = delegate { };
        public UnityAction onMineGemCapacityCleared = delegate { };


        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };

        //-----areas
        public Func<SaveLoadStates, int[]> onGetAreasCount = delegate { return null; };



    }
}