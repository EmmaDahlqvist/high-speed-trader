using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedFogController : MonoBehaviour
{
    public Material fogMaterial;  // Materialet som anv�nder shadern
    public Transform fogCenter;  // Transform som definierar dimmans centrum
    public float fogStartDistance = 10f;  // Startdistanse f�r dimman
    public float fogEndDistance = 50f;  // Slutdistanse f�r dimman

    void Update()
    {
        if (fogMaterial != null && fogCenter != null)
        {
            // Uppdatera v�rdet f�r _FogCenter i shadern baserat p� fogCenter-objektets position
            fogMaterial.SetVector("_FogCenter", fogCenter.position);
            fogMaterial.SetFloat("_FogStart", fogStartDistance);
            fogMaterial.SetFloat("_FogEnd", fogEndDistance);
        }
    }
}
