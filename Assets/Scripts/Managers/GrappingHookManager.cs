using UnityEngine;
using Signals;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using System;

public class GrappingHookManager: MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector3 grapplePoint;
    [SerializeField] private LayerMask whatIsGrappleable;

    #endregion

    #region Private Variables
    private LineRenderer _lr;

    private bool _isGrapping = false;
    private GraplingData _data;
    #endregion

    #endregion


    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onClicked += OnClicked;
        InputSignals.Instance.onInputReleased += OnReleased;
    }

    private void UnsubscribeEvents()
    {
        InputSignals.Instance.onClicked -= OnClicked;
        InputSignals.Instance.onInputReleased -= OnReleased;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    void Awake()
    {
        Init();
        StopGrapple();
    }

    private void Init()
    {
        _lr = GetComponent<LineRenderer>();
        _data = GetData();

    }
    private GraplingData GetData() => Resources.Load<CD_Grapling>("Data/CD_Grapling").GraplingData;


    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isGrapping)
        {
            return;
        }
        if (other.CompareTag("Grappable"))
        {
            grapplePoint = new Vector3(other.transform.position.x, other.transform.position.y - 8, other.transform.position.z);
        }
    }
    void StartGrapple()
    {
        _lr.positionCount = 2;
        currentGrapplePosition = startPoint.position;
    }

    void StopGrapple()
    {
        _lr.positionCount = 0;
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (_lr.positionCount < 2)
        {
            return;
        }
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * _data.Speed);

        _lr.SetPosition(0, startPoint.position);
        _lr.SetPosition(1, currentGrapplePosition);
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private void OnClicked(bool isClicked)
    {
        StartGrapple();
        _isGrapping = true;

    }

    private void OnReleased(bool isClicked)
    {
        StopGrapple();
        _isGrapping = false;
    }
}