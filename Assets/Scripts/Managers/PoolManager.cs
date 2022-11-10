using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;
using Data.ValueObject;
using Data.UnityObject;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<GameObject> wallPrefabs;
    [SerializeField] private List<GameObject> wallPool;
    [SerializeField] private int amountWallToPool = 50;

    #endregion
    #region Private Variables
    private WallData _data;
    #endregion
    #endregion
    private WallData GetData() => Resources.Load<CD_Wall>("Data/CD_Wall").wallData;

    private void Awake()
    {
        Init();
        InitializeWallPool();
    }
    private void Init()
    {
        _data = GetData();

    }

    private void Start()
    {
        PoolSignals.Instance.onInitializeAmountOfPool?.Invoke(amountWallToPool);

    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetDarkWallFromPool += OnGetObject;
        PoolSignals.Instance.onGetLightWallFromPool += OnGetObject;

        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;
        PoolSignals.Instance.onGetAmount += OnGetAmount;
        CoreGameSignals.Instance.onRestartLevel += OnReset;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetDarkWallFromPool -= OnGetObject;
        PoolSignals.Instance.onGetLightWallFromPool -= OnGetObject;

        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;
        PoolSignals.Instance.onGetAmount -= OnGetAmount;
        CoreGameSignals.Instance.onRestartLevel -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializeWallPool()
    {
        wallPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountWallToPool; i++)
        {
            tmp = Instantiate(wallPrefabs[i%2], transform); // 0||1 -> light||dark
            tmp.transform.position = new Vector3(0, Random.Range(_data.Y_MinRandomPos, _data.Y_MaxRandomPos), i * _data.WallZAxisLenght);
            wallPool.Add(tmp);
        }
    }

    public GameObject OnGetObject()
    {

        for (int i = 0; i < amountWallToPool; i++)
        {
            if (!wallPool[i].activeInHierarchy)
            {
                return wallPool[i];
            }
        }
        return null;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }

    public int OnGetAmount()
    {
        return amountWallToPool;
    }
    private void OnReset()
    {
        for (int i = 0; i < amountWallToPool; i++)
        {
            wallPool[i].transform.position = new Vector3(0, Random.Range(_data.Y_MinRandomPos, _data.Y_MaxRandomPos), i * _data.WallZAxisLenght);
            wallPool[i].SetActive(true);
        }

        PoolSignals.Instance.onPoolReseted?.Invoke();
    }
}