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
        public UnityAction<int,SaveLoadStates,SaveFiles> onSaveMoney = delegate { };
        public UnityAction<int, SaveLoadStates, SaveFiles> onChangeSoundState = delegate { };
        public UnityAction<int, SaveLoadStates, SaveFiles> onSelectTarget = delegate { };
        public UnityAction<List<int>> onSendOpenedItems = delegate { };
        public UnityAction<List<int>> onChangeOpenedItems = delegate { };


        public Func<SaveLoadStates,SaveFiles,int> onGetScore = delegate { return 0; };
        public Func<SaveLoadStates,SaveFiles,int> onGetMoney = delegate { return 0; };
        public Func<SaveLoadStates,SaveFiles,int> onGetSoundState = delegate { return 0; };
        public Func<SaveLoadStates,SaveFiles,int> onGetSelectedTargetId = delegate { return 0; };
        public Func<SaveLoadStates,SaveFiles,List<int>> onGetOpenedItems = delegate { return null; };

    }
}