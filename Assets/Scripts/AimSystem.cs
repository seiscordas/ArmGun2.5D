using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] private bool _isActiveAim = false;
    public bool IsActiveAim { get => _isActiveAim; }
    [SerializeField] private GameObject _player;
    public GameObject Player { get => _player; set => _player = value; }
    private RigBuilder _rigBuilder;
    [Header("Head Settings")]
    [SerializeField] private GameObject _head;
    [SerializeField] private float _headClampUp;
    [SerializeField] private float _headClampDown;
    [SerializeField] private float _headOffset;
    [Header("Spine Settings")]
    [SerializeField] private GameObject _rigSpine;
    [SerializeField] private float _spineClampUp;
    [SerializeField] private float _spineClampDown;
    [SerializeField] private float _spineOffset;
    [Space(10)]


    //debug inspector
    [Header("Debug Inspector")]
    public float DebugAngulo;
    public float DebugClamp;
    public Vector2 DebugAimPosition;
    public string DebugAny;

    void Start()
    {
        //_rigBuilder = Player.GetComponent(typeof(RigBuilder)) as RigBuilder;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_isActiveAim)
            {

                //Fire();
            }
            else
            {

                //FireAim();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _isActiveAim = !_isActiveAim;
            _aim.gameObject.SetActive(_isActiveAim);
        }

        if (!_isActiveAim)
            return;
        AimFollow();
        HeadAimFollow();
        //SpineBend();

    }
    private void HeadAimFollow()
    {
        if (IsFliping())
            return;
        float angle = Angle(_aim.position, _head.transform.position) - _headOffset;
        DebugAngulo = angle;
        if (angle > _headClampUp)
            angle = _headClampUp;
        if (angle < _headClampDown)
            angle = _headClampDown;
        _head.transform.eulerAngles = new Vector3(angle, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
        DebugAny = _head.transform.eulerAngles.ToString();
        FixHeadPositionAim(angle);
    }
    private void ResetHeadPosition()
    {
        //_head.transform.eulerAngles = new Vector3(-39.534f, -3.806f, -18.956f);
    }
    private void FixHeadPositionAim(float angle)
    {
        if (angle != _head.transform.eulerAngles.x)
            _head.transform.eulerAngles = new Vector3(angle, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
    }
    private void SpineBend(float clampUp, float clampDown, float offset)
    {
        float angle = Angle(_aim.position, _head.transform.position);
        angle = Mathf.Clamp(angle, clampUp, clampDown);
        _rigSpine.transform.eulerAngles = new Vector3(angle - offset, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
    }
    private float Angle(Vector3 point1, Vector3 point2)
    {
        Vector3 _points = point1 - point2;
        _points.Normalize();
        if (!IsFacingRight())
            _points.x *= -1;
        float angle = Mathf.Atan2(_points.y, _points.x) * Mathf.Rad2Deg;
        return angle;
    }
    private void AimFollow()
    {
        Vector2 aim = MousePosition;
        _aim.transform.position = new Vector2(aim.x, aim.y);
        DebugAimPosition = _aim.transform.position = new Vector2(aim.x, aim.y);///DEBUG INSPECTOR
    }
    private Vector2 MousePosition
    {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    }
    private bool IsFacingRight()
    {
        return _player.GetComponent<Player3DController>().facingRight;
    }
    private bool IsFliping()
    {
        return _player.GetComponent<Player3DController>()._isFliping;
    }
}
