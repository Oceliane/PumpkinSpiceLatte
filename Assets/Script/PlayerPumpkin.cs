using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPumpkin : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] MovementController refMouvementController;
    [SerializeField] RoomsManager refRoomsManager;
    [SerializeField] bool isHidden;

    [SerializeField] GameObject seedPrefab;
    GameObject projectile;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isHidden)
            {
                isHidden = false;
                playerAnimator.SetTrigger("MaskOff");
                playerAnimator.SetBool("HasMaskOn", false);

                refRoomsManager.PlayerStatusChanged(isHidden);
            }
            else
            {
                isHidden = true;
                playerAnimator.SetTrigger("MaskOn");
                playerAnimator.SetBool("HasMaskOn", true);

                refRoomsManager.PlayerStatusChanged(isHidden);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && projectile == null)
        {
            projectile = Instantiate(seedPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Seed>().moveDir = refMouvementController.lastDir;
        }
    }
}
