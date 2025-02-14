using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Vehicle"))
        {
            // L�t bilen passera (ingen �tg�rd beh�vs)
        }
        else
        {
            // Stoppa allt annat
            other.gameObject.SetActive(false);
        }
    }
}
