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
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public Func<int> onGetCurrentLevel = delegate { return 0; };
        public Func<int> onGetTotalLevelCount = delegate { return 0; };
    }
}