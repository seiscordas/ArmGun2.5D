using UnityEngine;

namespace kl
{
    public class AimSystem : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        [SerializeField] private Transform _aim;
        public Transform Aim { get => _aim; }
        [SerializeField] static bool _activeAim = false;
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

        [SerializeField] private Camera _cam;
        private float _aimPositionX;
        private float dinamicOffset;
        private void Start()
        {
            characterControl.AimPosition = _aim;
            dinamicOffset = _armGunOffset;
            characterControl.ActiveAim = false;
        }
        void Update()
        {
            if (Input.GetButtonDown("Fire2") && !characterControl.Fliping)
            {
                _activeAim = !_activeAim;
                _aim.gameObject.SetActive(_activeAim);
                characterControl.ActiveAim = _activeAim;
                VirtualInputManager.Instance.ActiveAim = _activeAim;
            }
            if (_activeAim)
            {
                AimGun();
                HeadAimFollow();
                ArmGunAimFollow();
                GunAimFollow();
            }
            _aimPositionX = _aim.position.x - transform.position.x;
            if (_activeAim && _aimPositionX < 0 && characterControl.FacingRight)
            {
                VirtualInputManager.Instance.FlipToLeft = true;
                characterControl.Fliping = true;
            }
            if (_activeAim && _aimPositionX > 0 && !characterControl.FacingRight)
            {
                VirtualInputManager.Instance.FlipToRight = true;
                characterControl.Fliping = true;
            }
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
            if (characterControl.FacingRight)
            {
                _armGun.transform.eulerAngles = new Vector3(0f, 0f, dinamicAngleOffset);
            }
            else
            {
                _armGun.transform.eulerAngles = new Vector3(0f, 0f, -dinamicAngleOffset);
            }
            debug = dinamicAngleOffset + "";
        }
        private void GunAimFollow()
        {
            float angle = Angle(_aim.position, _gun.position) - _gunOffset;
            if (characterControl.FacingRight)
            {
                _gun.transform.eulerAngles = new Vector3(0f, 0f, angle);
            }
            else
            {
                _gun.transform.eulerAngles = new Vector3(0f, 0f, -angle);
            }
        }
        private float Angle(Vector3 point1, Vector3 point2)
        {
            Vector3 _points = point1 - point2;
            _points.Normalize();
            if (!characterControl.FacingRight)
            {
                _points.x *= -1;
            }
            return Mathf.Atan2(_points.y, _points.x) * Mathf.Rad2Deg;
        }
        [SerializeField] private string debug;

        private void AimGun()
        {
            //TODO: TIRAR NFREEZE ? S?MENTE PARA DEBUG
            bool nfreeze = true;
            if (Input.GetKey(KeyCode.F))
                nfreeze = !nfreeze;
            if (!nfreeze)
            {
                //_aim.position = new Vector3(MousePosition.x, MousePosition.y, -0.2f) - _mouseOffset;
            }
            if (characterControl.FacingRight)
            {
                _aim.position = new Vector3(MousePosition.x, MousePosition.y, -0.2f);
            }
            else
            {
                _aim.position = new Vector3(MousePosition.x, MousePosition.y, 0.2f);
            }
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
            get
            {
                return _cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}

//TODO



//BKP
/*
 
 using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    public Transform Aim { get => _aim; }
    [SerializeField] static bool _isActiveAim = false;
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




    [SerializeField] private Camera _cam;
    void Update()
    {
        Debug.Log(MousePosition);
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
        //GunAimFollow();
    }
    void FixedUpdate()
    {
        if (!_isActiveAim && IsFliping() == false)
            return;
        AimGun();
        HeadAimFollow();
        ArmGunAimFollow();
        //GunAimFollow();
    }
    [SerializeField] private string debugAngle;
    private void ArmGunAimFollow()
    {
        float dinamicOffset = Mathf.Round(Quaternion.Angle(_armGun.rotation, _aim.rotation));

        dinamicOffset = Mathf.Clamp(dinamicOffset, 120, 180);
        Quaternion lookAt = Quaternion.LookRotation(_aim.position - _armGun.position);
        Quaternion correction = Quaternion.Euler(dinamicOffset, 0f, 0f);
        _armGun.rotation = (lookAt * correction);
        //_armGun.rotation = new Quaternion(Mathf.Clamp(_armGun.rotation.x, 80f, 80f), _armGun.rotation.y, _armGun.rotation.z, _armGun.rotation.w);

        debugAngle = _armGun.rotation.z + "//" + (_armGun.rotation.x * 100).ToString() + "//" + correction.ToString();
        Debug.DrawRay(_armGun.position, _aim.position - _armGun.position, Color.green);

        //TODO: TORNAR OFFISET DINAMICO PARTINDO DO MEIO IR BAIXANDO O OFFSET PARA 120 CONFOME VAI SUBINDO A MIRA E AUMENTAR PARA 180 CONFOME BAIXA


        //float dinamicOffset = 0f;
        //float angle = Quaternion.Angle(_aim.rotation, _armGun.rotation);
        //angle = Mathf.Abs(angle);
        //if (angle > 120 && angle < 180)
        //{
        //    dinamicOffset = angle;
        //}
        //dinamicOffset = Mathf.Clamp(dinamicOffset, 120, 180);
        //debugAngle = angle.ToString();
        //float dinamicAngleOffset = Quaternion.Angle(_armGun.rotation, _aim.rotation) - dinamicOffset;
        //if (dinamicAngleOffset < _armGunClampDown)
        //    dinamicAngleOffset = _armGunClampDown;
        //_armGun.transform.eulerAngles = new Vector3(_armGun.eulerAngles.x, _armGun.eulerAngles.y, angle);
    }
    private void GunAimFollow()
    {
        //Quaternion lookAt = Quaternion.LookRotation(_aim.position - _gun.position);
        //Quaternion correction = Quaternion.Euler(_gunOffset, 0f, 0f);
        //_gun.rotation = lookAt * correction;

        Debug.DrawRay(_gun.position, _aim.position - _gun.position, Color.black);
    }
    private float Angle(Vector3 point1, Vector3 point2)
    {
        Vector3 _points = point1 - point2;
        _points.Normalize();
        //if (!IsFacingRight())
        // _points.x *= -1;
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
        //_aim.position = new Vector3(MousePosition.x, MousePosition.y, -0.19f);
        _aim.position = new Vector3(0.2f, MousePosition.y, MousePosition.z);
    }
    private void HeadAimFollow()
    {
        Quaternion lookAt = Quaternion.LookRotation(_aim.position - _head.position);
        Quaternion correction = Quaternion.Euler(_headOffset, 0f, 0f);
        _head.rotation = (lookAt * correction);
        //_head.rotation = new Quaternion(Mathf.Clamp(_armGun.rotation.x, _headClampUp, _headClampDown), _armGun.rotation.y, _armGun.rotation.z, _armGun.rotation.w);


        //float angle = Angle(_aim.position, _head.transform.position) - _headOffset;
        //if (angle > _headClampUp)
        //    angle = _headClampUp;
        //if (angle < _headClampDown)
        //    angle = _headClampDown;
        //_head.transform.eulerAngles = new Vector3(angle, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
    }
    private Vector3 MousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            //mousePos.z = _cam.nearClipPlane;
            //return _cam.ScreenToWorldPoint(Input.mousePosition);
            return _cam.ScreenToWorldPoint(mousePos);
        }
    }
    private bool IsFacingRight()
    {
        return _player.GetComponent<Player3DController>()._facingRight;
    }
    private bool IsFliping()
    {
        return _player.GetComponent<Player3DController>()._isFliping;
    }

    public bool IsActiveAim
    {
        get
        {
            return _isActiveAim;
        }

        set
        {
            _isActiveAim = value;
        }
    }
}
//TODO
 
 
 */