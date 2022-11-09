using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;


namespace Managers
{

    public class WallManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables
        [SerializeField] private int maksDistance = 10;
        [SerializeField] private MeshRenderer mesh1, mesh2, mesh3;
        [SerializeField] private int colorType = 0;

        #endregion

        #region Private Variables
        private int _levelId = 0;
        private int _totalLevelCount = 0;
        #endregion

        #endregion

        #region Event Subscription


        private void Init()
        {
            _totalLevelCount = LevelSignals.Instance.onGetTotalLevelCount();

            ResetMaterial();
            
        }
        private void ResetMaterial()
        {
            mesh1.material = Resources.Load(("Materials/" + (_levelId + 1).ToString() + "/" + colorType).ToString()) as Material;
            mesh2.material = Resources.Load(("Materials/" + (_levelId + 1).ToString() + "/" + colorType).ToString()) as Material;
            mesh3.material = Resources.Load(("Materials/" + (_levelId + 1).ToString() + "/" + colorType).ToString()) as Material;
        }

        private void OnEnable()
        {
            SubscribeEvents();
            ResetMaterial();
        }

        private void Start()
        {
            Init();

        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onPoolReseted += OnPoolReseted;
        }



        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onPoolReseted -= OnPoolReseted;

        }

        private void OnDisable()
        {
            if (_totalLevelCount - 1 == _levelId)
            {
                _levelId = 0;
            }
            else
            {
                ++_levelId;
            }
            UnsubscribeEvents();
        }

        #endregion

        private void OnPoolReseted()
        {
            _levelId = 0;
            ResetMaterial();
        }
    }
}