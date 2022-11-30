using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public Func<int> onGetScore = delegate { return 0; };
        public Func<int> onGetMoney = delegate { return 500; };

        public UnityAction<ScoreTypeEnums, int> onScoreIncrease = delegate { };
        public UnityAction<ScoreTypeEnums, int> onScoreDecrease = delegate { };

        public UnityAction<ScoreTypeEnums, int> onMoneyIncreased = delegate { };
        public UnityAction<ScoreTypeEnums, int> onMoneyDecrease = delegate { };

        public UnityAction onHighScore = delegate { };
        public UnityAction<int> onHitTarget = delegate { };
        public UnityAction onMissTarget = delegate { };



    }
}