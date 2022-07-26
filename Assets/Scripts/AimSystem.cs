using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    public Transform Aim { get => _aim; }
    [SerializeField] private bool _isActiveAim = false;
    public bool IsActiveAim { get => _isActiveAim; }
    [SerializeField] private GameObject _player;
    public GameObject Player { get => _player; set => _player = value; }
    [Header("Head Settings")]
    [SerializeField] private Transform _head;
    [SerializeField] private float _headClampUp;
    [SerializeField] private float _headClampDown;
    [SerializeField] private float _headOffset;
    [Header("Arm Gun Settings")]
    [SerializeField] private Transform _armGun;
    [SerializeField] private float _armGunOffset;
    [SerializeField] private float _armGunClampDown;
    [Header("Gun Settings")]
    [SerializeField] private Transform _gun;
    [SerializeField] private float _gunOffset;

    private Camera _camera;
    private float dinamicOffset;
    private void Start()
    {
        _camera = Camera.main;
        dinamicOffset = _armGunOffset;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _isActiveAim = !_isActiveAim;
            _aim.gameObject.SetActive(_isActiveAim);
        }
        if (!_isActiveAim && IsFliping() == false)
            return;
        AimGun();
        HeadAimFollow();
        ArmGunAimFollow();
        GunAimFollow();
    }
    private void ArmGunAimFollow()
    {
        float angle = Angle(_aim.position, _armGun.position) - _armGunOffset;
        angle = Mathf.Abs(angle);
        if (angle > 120 && angle < 180)
        {
            dinamicOffset = angle;
        }
        float dinamicAngleOffset = Angle(_aim.position, _armGun.position) - dinamicOffset;
        if (dinamicAngleOffset < _armGunClampDown)
            dinamicAngleOffset = _armGunClampDown;
        _armGun.transform.eulerAngles = new Vector3(_armGun.eulerAngles.x, _armGun.eulerAngles.y, dinamicAngleOffset);
    }
    private void GunAimFollow()
    {
        float angle = Angle(_aim.position, _gun.position) - _gunOffset;
        float teste = angle * Mathf.Deg2Rad;
        _gun.transform.eulerAngles = new Vector3(_gun.eulerAngles.x, _gun.eulerAngles.y, angle);
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
    private void AimGun()
    {
        //TODO: TIRAR NFREEZE É SÓMENTE PARA DEBUG
        bool nfreeze = true;
        if (Input.GetKey(KeyCode.F))
            nfreeze = !nfreeze;
        if (!nfreeze)
        {
        }
        _aim.position = new Vector3(MousePosition.x, MousePosition.y, -0.19f);
    }
    private void HeadAimFollow()
    {
        float angle = Angle(_aim.position, _head.transform.position) - _headOffset;
        if (angle > _headClampUp)
            angle = _headClampUp;
        if (angle < _headClampDown)
            angle = _headClampDown;
        _head.transform.eulerAngles = new Vector3(-angle, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
    }
    private Vector3 MousePosition
    {
        get { return _camera.ScreenToWorldPoint(Input.mousePosition); }
    }
    private bool IsFacingRight()
    {
        return _player.GetComponent<Player3DController>()._facingRight;
    }
    private bool IsFliping()
    {
        return _player.GetComponent<Player3DController>()._isFliping;
    }
}
//TODO