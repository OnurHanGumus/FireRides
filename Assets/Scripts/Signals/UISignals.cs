using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        public UnityAction<int> onUpdateStageData;
        public UnityAction<int> onSetLevelText;
        public UnityAction<bool> onCloseSuccessfulPanel;

        public UnityAction<ScoreTypeEnums, int> onSetChangedText;


        public UnityAction<List<int>, SaveLoadStates, SaveFiles> onInitializeGunLevels;
        public UnityAction<List<int>, SaveLoadStates, SaveFiles> onChangeGunLevels;


        public Func<List<int>> onGetGunLevels = delegate { return null; };
    }
}