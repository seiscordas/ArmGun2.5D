using UnityEngine;

public class HittableObj : MonoBehaviour, IShotHit
{
    private GameObject hitGameObject;
    [Range(1f, 20f)]
    [SerializeField] private float _minForce = 10f;
    [Range(1f, 20f)]
    [SerializeField] private float _maxForce = 15f;

    private Rigidbody _rigidbody;

    void Start()
    {
        float force = Random.Range(_minForce, _maxForce);
        hitGameObject = this.gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
    }
    void IShotHit.Hit() { hitGameObject.SetActive(false); }

    private void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
    }

}
