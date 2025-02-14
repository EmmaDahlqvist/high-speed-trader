using UnityEngine;
using DG.Tweening;

public class VaultOverObject : MonoBehaviour
{
    public float vaultDuration = 0.5f;
    public float vaultHeight = 1.2f;
    public float vaultDistance = 3f;
    public float cameraTiltAngle = 15f;
    public float cameraTiltDuration = 0.3f;

    private Transform player;
    private Transform playerObj;
    private Transform playerCam; // Länka din kamera här i Unity Inspector
    private PlayerMovement pm;

    private bool isVaulting = false;

    void Start()
    {
        playerCam = GameObject.Find("PlayerCam").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isVaulting)
        {
            return;
        }

        // player was in trigger zone
        if (other.CompareTag("Player"))
        {
            isVaulting = true;
            playerObj = other.transform;
            player = other.transform.root;
            pm = playerObj.parent.GetComponent<PlayerMovement>(); // get playermovement script from player

            PerformVault();
        }
    }

    void PerformVault()
    {
        pm.vaulting = true;
        Vector3 startPos = player.position;
        Vector3 endPos = startPos + (playerCam.forward * vaultDistance) + (Vector3.up * vaultHeight);

        Quaternion originalCamRot = playerCam.localRotation;

        // Gör kameratilten
        playerCam.DOLocalRotate(new Vector3(cameraTiltAngle, 0, 0), cameraTiltDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
                playerCam.DOLocalRotate(Vector3.zero, cameraTiltDuration).SetEase(Ease.InOutSine) // Återställ kameran
            );

        // Flytta spelaren med DOTween
        player.DOMove(endPos, vaultDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => { isVaulting = false;
                pm.vaulting = false;
            });
    }
}
