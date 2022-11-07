using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<GameObject> wallPrefabs;



    [SerializeField] private List<GameObject> wallDarkPool;
    [SerializeField] private List<GameObject> wallLightPool;



    [SerializeField] private int amountWallToPool = 50;



    #endregion
    #region Private Variables
    private int _levelId = 0;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        InitializeWallPool();
    }

    

    #region Event Subscriptions
    void Start()
    {

    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetDarkWallFromPool += OnGetDarkWall;
        PoolSignals.Instance.onGetLightWallFromPool += OnGetLightWall;



        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;


        //PlayerSignals.Instance.onPlayerSelectGun += OnPlayerSelectGun;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetDarkWallFromPool -= OnGetDarkWall;
        PoolSignals.Instance.onGetLightWallFromPool -= OnGetLightWall;

        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;


        //PlayerSignals.Instance.onPlayerSelectGun -= OnPlayerSelectGun;


    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion



    private void InitializeWallPool()
    {
        wallDarkPool = new List<GameObject>();
        wallLightPool = new List<GameObject>();
        GameObject tmp, tmp1;
        for (int i = 0; i < 25; i++)
        {
            tmp = Instantiate(wallPrefabs[0], transform);
            tmp1 = Instantiate(wallPrefabs[1], transform);

            tmp.transform.position = new Vector3(0, Random.Range(-4, 5), (2*i+1)*2);


            tmp1.transform.position = new Vector3(0, Random.Range(-4, 5), (i * 4));

            wallLightPool.Add(tmp);
            wallDarkPool.Add(tmp1);
        }
    }

  
    public GameObject OnGetDarkWall()
    {
 
        for (int i = 0; i < amountWallToPool; i++)
        {
            if (!wallDarkPool[i].activeInHierarchy)
            {
                return wallDarkPool[i];
            }
        }
        return null;
    }
    public GameObject OnGetLightWall()
    {

        for (int i = 0; i < amountWallToPool; i++)
        {
            if (!wallLightPool[i].activeInHierarchy)
            {
                return wallLightPool[i];
            }
        }
        return null;
    }

 

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }



    private void OnReset()
    {
        //reset
    }


}
