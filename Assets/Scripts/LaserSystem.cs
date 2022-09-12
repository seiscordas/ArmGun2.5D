using System.Collections;
using UnityEngine;
namespace kl
{
    public class LaserSystem : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        [SerializeField] Transform _aimTarget;
        [SerializeField] Color _laserColor = Color.red;
        [SerializeField] int _intensity;
        [SerializeField] int _distance;
        [SerializeField] float startWidth = 0.02f, endWidth = 0.01f;
        private GameObject _lightHit;
        private Vector3 _lightPosition;
        private LineRenderer _lineRenderer;
        private Player3DController _player;
        private AimSystem aimSystem;

        public Player3DController Player { get => _player; set => _player = value; }

        void Start()
        {
            aimSystem = _player.gameObject.GetComponent<AimSystem>();
            _lightHit = new GameObject();
            _lightHit.AddComponent<Light>();
            _lightHit.GetComponent<Light>().intensity = 8;
            _lightHit.GetComponent<Light>().range = endWidth * 2;
            _lightHit.GetComponent<Light>().color = _laserColor;
            _lightPosition = new Vector3(0, endWidth, 0);
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
            _lineRenderer.startColor = _laserColor;
            _lineRenderer.endColor = _laserColor;
            _lineRenderer.startWidth = startWidth;
            _lineRenderer.endWidth = endWidth;
            _lineRenderer.positionCount = 2;
        }
        void Update()
        {
            if (characterControl.ActiveAim && !characterControl.Fliping)
            {
                ShotSpawnLookAtAimTarget();
                if (Input.GetMouseButtonDown(0))
                    StartCoroutine("ShotLaser");
            }
        }

        private IEnumerator ShotLaser()
        {
            Vector3 LaserEndPoint = transform.position + transform.forward * _distance;
            RaycastHit HitPoint;
            if (Physics.Raycast(transform.position, transform.forward, out HitPoint, _distance))
            {
                IShotHit hittedObj = HitPoint.transform.GetComponent<IShotHit>();
                if (hittedObj != null)
                    hittedObj.Hit();
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, HitPoint.point);
                _lightHit.transform.position = HitPoint.point;
                _lightHit.transform.position = (HitPoint.point - _lightPosition);
            }
            else
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, LaserEndPoint);
            }
            _lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            _lineRenderer.enabled = false;
        }

        void ShotSpawnLookAtAimTarget()
        {
            transform.LookAt(_aimTarget);
        }
    }
}
