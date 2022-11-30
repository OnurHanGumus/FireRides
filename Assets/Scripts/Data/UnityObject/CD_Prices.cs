using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Price", menuName = "Picker3D/CD_Price", order = 0)]
    public class CD_Prices : ScriptableObject
    {
        public PriceData priceData;
    }
}