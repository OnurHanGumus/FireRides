using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Wall", menuName = "Picker3D/CD_Wall", order = 0)]
    public class CD_Wall : ScriptableObject
    {
        public WallData wallData;
    }
}