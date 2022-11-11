using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class StoreSignals : MonoSingleton<StoreSignals>
    {
        public UnityAction<int> onSendSelectedTarget = delegate {  };
    }
}