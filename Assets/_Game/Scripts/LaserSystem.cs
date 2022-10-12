using System;
using System.Collections;
using UnityEngine;
namespace kl
{
    public class LaserSystem : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        [SerializeField] Transform _aimTarget;
        [SerializeField] Transform _shotSpawn;
        [SerializeField] Color _startColor;
        [SerializeField] Color _endColor;
        [SerializeField] int _intensity;
        [SerializeField] int _distance;
        [SerializeField] float startWidth = 0.02f, endWidth = 0.01f;
        private GameObject _lightHit;
        private Vector3 _lightPosition;
        [SerializeField] private LineRenderer _lineRenderer;
        private Player3DController _player;
        //private AimSystem aimSystem;

        public Player3DController Player { get => _player; set => _player = value; }

        void Start()
        {
            _lineRenderer.enabled = false;
            _lightPosition = new Vector3(0, endWidth, 0);
            //aimSystem = _player.gameObject.GetComponent<AimSystem>();
            //_lightHit = new GameObject();
            //_lightHit.AddComponent<Light>();
            //_lightHit.GetComponent<Light>().intensity = _intensity;
            //_lightHit.GetComponent<Light>().range = endWidth * 2;
            //_lightHit.GetComponent<Light>().color = _startColor;
            //_lineRenderer = gameObject.AddComponent<LineRenderer>();
            //_lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
            //_lineRenderer.startColor = _startColor;
            //_lineRenderer.endColor = _endColor;
            //_lineRenderer.startWidth = startWidth;
            //_lineRenderer.endWidth = endWidth;
            //_lineRenderer.positionCount = 2;
        }
        public string _debug;
        void Update()
        {
            if (characterControl.ActiveAim)
            {
                _debug = _shotSpawn.parent.transform.eulerAngles + "";
                ShotSpawnLookAtAimTarget();
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine("ShotLaser");
                }
                Debug.DrawRay(_shotSpawn.position, _aimTarget.position, Color.blue);
            }
        }

        private IEnumerator ShotLaser()
        {
            _shotSpawn.parent.transform.eulerAngles = new Vector3(0f, 0f, _shotSpawn.parent.transform.eulerAngles.z);
            Vector3 LaserEndPoint = _shotSpawn.position + _shotSpawn.forward * _distance;
            RaycastHit HitPoint;
            if (Physics.Raycast(_shotSpawn.position, _shotSpawn.forward, out HitPoint, Mathf.Infinity))
            {
                IShotHit hittedObj = HitPoint.transform.GetComponent<IShotHit>();
                if (hittedObj != null)
                    hittedObj.Hit();
                _lineRenderer.SetPosition(0, _shotSpawn.position);
                _lineRenderer.SetPosition(1, HitPoint.point);
                //_lightHit.transform.position = HitPoint.point;
                //_lightHit.transform.position = (HitPoint.point - _lightPosition);
            }
            else
            {
                _lineRenderer.SetPosition(0, _shotSpawn.position);
                _lineRenderer.SetPosition(1, LaserEndPoint);
            }
            _lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.03f);
            _lineRenderer.enabled = false;
        }

        void ShotSpawnLookAtAimTarget()
        {
            _shotSpawn.LookAt(_aimTarget);
        }
    }
}
