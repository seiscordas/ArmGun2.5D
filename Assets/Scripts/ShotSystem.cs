using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoBehaviour
{
    private AimSystem _aimSystem;
    [Header("Fire Settings")]
    [SerializeField] private Transform _shotSpawn;
    [SerializeField] private GameObject _ballBullet;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _shotOffset;

    [SerializeField] private float _bulletVelocity = 0f;
    public Vector2 DebugDirection;
    void Start()
    {
        _aimSystem = this.gameObject.GetComponent<AimSystem>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && _aimSystem.IsActiveAim)
        {
            //StartCoroutine(ShotAim());
            //FireAimObj();
        }
        //Debug.DrawLine(_shotSpawn.transform.position, _aimSystem.Aim.position);
        DebugDirection = _aimSystem.Aim.position;
    }
    private IEnumerator ShotAim()
    {
        RaycastHit hitInfo;
        Vector3 direction = new Vector3(_aimSystem.Aim.position.x, _aimSystem.Aim.position.y - _shotOffset, _aimSystem.Aim.position.z);
        if (Physics.Raycast(_shotSpawn.transform.position, _aimSystem.Aim.position, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Hittable")))
        {
            IShotHit hittedObj = hitInfo.transform.GetComponent<IShotHit>();
            if (hittedObj != null)
                hittedObj.Hit();
            _lineRenderer.SetPosition(0, _shotSpawn.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
            Debug.DrawLine(_shotSpawn.transform.position, _aimSystem.Aim.position);
        }
        else
        {
            Debug.DrawLine(_shotSpawn.transform.position, _aimSystem.Aim.position);
            _lineRenderer.SetPosition(0, _shotSpawn.position);
            _lineRenderer.SetPosition(1, _shotSpawn.position + direction * 100);
        }
        
        _lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.1f);

        _lineRenderer.enabled = false;
    }
    void FireAimObj()
    {
        Vector3 direction = new Vector3(_aimSystem.Aim.position.x, _aimSystem.Aim.position.y - _shotOffset, _aimSystem.Aim.position.z);
        GameObject bullet = (GameObject)Instantiate(_ballBullet, _shotSpawn.position, _shotSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = direction * _bulletVelocity;
        Destroy(bullet, 1f);
    }
}
