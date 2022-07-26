using UnityEngine;

public class HittableObj : MonoBehaviour, IShotHit
{
    GameObject hitGameObject;
    void Start()
    {
        hitGameObject = this.gameObject;
    }
    void IShotHit.Hit() { hitGameObject.SetActive(false); }
}
