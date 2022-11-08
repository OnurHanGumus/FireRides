using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Grapling", menuName = "Picker3D/CD_Grapling", order = 0)]
    public class CD_Grapling : ScriptableObject
    {
        public GraplingData GraplingData;
    }
}