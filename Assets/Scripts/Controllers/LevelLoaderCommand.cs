using UnityEngine;

namespace Controllers
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(GameObject gameObject, Transform levelHolder, int level)
        {
            Instantiate(gameObject, new Vector3(0,0, (level * 178) + level * 2), Quaternion.identity, levelHolder);
        }
    }
}