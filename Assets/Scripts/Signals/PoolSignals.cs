using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<GameObject> onGetDarkWallFromPool = delegate { return null; };
        public Func<GameObject> onGetLightWallFromPool = delegate { return null; };


        public Func<Transform> onGetPoolManagerObj = delegate { return null; };

        public UnityAction<GameObject> onAddBulletToPool = delegate { };


    }
}