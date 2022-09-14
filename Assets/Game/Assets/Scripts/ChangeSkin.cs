using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public Material Material;
    public void ChangeMaterial()
    {
        if (Material == null)
        {
            Debug.LogError("No material specified");
        }
        Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();
        foreach (var r in arrMaterials)
        {
            if (r.gameObject != this.gameObject)
            {
                r.material = Material;
            }
        }
    }
}
