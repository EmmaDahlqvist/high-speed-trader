using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedFogController : MonoBehaviour
{
    public Material fogMaterial;  // Materialet som använder shadern
    public Transform fogCenter;  // Transform som definierar dimmans centrum
    public float fogStartDistance = 10f;  // Startdistanse för dimman
    public float fogEndDistance = 50f;  // Slutdistanse för dimman

    void Update()
    {
        if (fogMaterial != null && fogCenter != null)
        {
            // Uppdatera värdet för _FogCenter i shadern baserat på fogCenter-objektets position
            fogMaterial.SetVector("_FogCenter", fogCenter.position);
            fogMaterial.SetFloat("_FogStart", fogStartDistance);
            fogMaterial.SetFloat("_FogEnd", fogEndDistance);
        }
    }
}
