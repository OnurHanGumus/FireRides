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
    public Vector3 GrapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;

    #endregion

    #region Serialized Variables


    #endregion

    #region Private Variables
    private LineRenderer lr;

    private float maxDistance = 1000f;
    private SpringJoint joint;
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
        lr = GetComponent<LineRenderer>();
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
            GrapplePoint = new Vector3(other.transform.position.x, other.transform.position.y - 8, other.transform.position.z);
        }
    }
    void StartGrapple()
    {
        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (lr.positionCount < 2)
        {
            return;
        }
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, GrapplePoint, Time.deltaTime * _data.Speed);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return GrapplePoint;
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