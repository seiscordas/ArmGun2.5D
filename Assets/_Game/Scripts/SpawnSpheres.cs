using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpheres : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Transform _hitableObj;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _minSpawnDelay = 1f;
    [SerializeField] private float _maxSpawnDelay = 5f;
    [SerializeField] private Quaternion quaternion;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float timer = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            yield return new WaitForSeconds(timer);
            GameObject obj = Instantiate(_sphere, _spawnPoint.position, _spawnPoint.rotation, _hitableObj);

            yield return new WaitForSeconds(timer);
            Destroy(obj, 5f);
        }
    }


}
