using System.Collections;
using UnityEngine;

namespace kl
{
    public class AimSystem : MonoBehaviour
    {
        [Header("Crosshair")]
        [SerializeField] private Texture2D _crosshair;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] private Vector2 hotSpot = Vector2.zero;
        [SerializeField] private Transform _aim;

        [SerializeField] private CharacterControl characterControl;
        [SerializeField] private static bool _activeAim = false;
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
        [Header("General Settings")]
        [SerializeField] private Vector2 dinamicOffsetClamp;

        [SerializeField] private Camera _cam;
        private float _aimPositionX;
        private float dinamicOffset;
        public Transform Aim { get => _aim; }

        private LaserSystem laserSystem;
        
        private void Start()
        {
            Cursor.SetCursor(_crosshair, hotSpot, cursorMode);
            characterControl.AimPosition = _aim;
            dinamicOffset = _armGunOffset;
            characterControl.ActiveAim = false;
            laserSystem = GetComponent<LaserSystem>();
        }
        void Update()
        {
            if (Input.GetButtonDown("Fire2") && !characterControl.Fliping)
            {
                ToggleActiveAimMode();
            }
            if (_activeAim)
            {
                DoAim();
            }
            StartFlipOnAimState();
        }

        private void ToggleActiveAimMode()
        {
            _activeAim = !_activeAim;
            _aim.gameObject.SetActive(_activeAim);
            characterControl.ActiveAim = _activeAim;
            VirtualInputManager.Instance.ActiveAim = _activeAim;
            laserSystem.enabled = _activeAim;
            //Cursor.visible = !_activeAim;
        }

        private void StartFlipOnAimState()
        {
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

        private void DoAim()
        {
                AimGun();
                ArmGunAimFollow();
                GunAimFollow();
                HeadAimFollow();
        }

        private void ArmGunAimFollow()
        {
            float angle = Angle(_aim.position, _armGun.position) - _armGunOffset;
            angle = Mathf.Abs(angle);
            dinamicOffset = Mathf.Clamp(angle, 120, 180);
            float dinamicAngleOffset = Angle(_aim.position, _armGun.position) - dinamicOffset;
            dinamicAngleOffset = Mathf.Clamp(dinamicAngleOffset, _armGunClampDown, dinamicAngleOffset);

            _armGun.eulerAngles = new Vector3(0f, 0f, characterControl.FacingRight ? dinamicAngleOffset : -dinamicAngleOffset);
            debug = dinamicAngleOffset + "//" + dinamicOffset;
        }
        private void GunAimFollow()
        {
            float angle = Angle(_aim.position, _gun.position) - _gunOffset;
            _gun.eulerAngles = new Vector3(0f, 0f, characterControl.FacingRight ? angle : -angle);
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
            //TODO: TIRAR NFREEZE ? SOMENTE PARA DEBUG
            bool nfreeze = true;
            if (Input.GetKey(KeyCode.F))
                nfreeze = !nfreeze;
            if (!nfreeze)
            {
                //_aim.position = new Vector3(MousePosition.x, MousePosition.y, characterControl.FacingRight ? -0.2f : 0.2f);
            }
            _aim.position = new Vector3(MousePosition.x, MousePosition.y, characterControl.FacingRight ? -0.2f : 0.2f);
        }
        private void HeadAimFollow()
        {
            float angle = Angle(_aim.position, _head.transform.position) - _headOffset;
            angle = Mathf.Clamp(angle, _headClampDown, _headClampUp);
            _head.eulerAngles = new Vector3(-angle, _head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
        }
        private Vector3 MousePosition
        {
            get
            {
                return _cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public Texture2D Crosshair { get => _crosshair; set => _crosshair = value; }
    }
}