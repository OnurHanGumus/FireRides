using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(GameObject gameObject, Transform levelHolder, int level, WallData wallData, int poolCount)
        {
            Instantiate(gameObject, new Vector3(0,0, (level * (wallData.WallZAxisLenght * poolCount))), Quaternion.identity, levelHolder);
        }
    }
}