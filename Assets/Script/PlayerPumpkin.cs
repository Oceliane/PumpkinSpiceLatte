using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPumpkin : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] MovementController refMouvementController;
    [SerializeField] RoomsManager refRoomsManager;
    [SerializeField] bool isHidden;
    [SerializeField] float delayWhenMasked;
    [SerializeField] float delayWhenNotMasked;
    private PlayerInputAction playerControls;

    [SerializeField] GameObject seedPrefab;
    GameObject projectile;

    private void Start()
    {
        playerControls = InputManager.inputActions;
        playerControls.Player.Enable();
        playerControls.Player.Helmet.started += Anim_Pumpkin;
        playerControls.Player.Fire.started += Fire_Seed;
    }

    private void Anim_Pumpkin(InputAction.CallbackContext context)
    {
        if (isHidden)
        {
            isHidden = false;
            playerAnimator.SetTrigger("MaskOff");
            playerAnimator.SetBool("HasMaskOn", false);
            refMouvementController.delayBetweenMovement = delayWhenNotMasked;

            refRoomsManager.PlayerStatusChanged(isHidden);
        }
        else
        {
            isHidden = true;
            playerAnimator.SetTrigger("MaskOn");
            playerAnimator.SetBool("HasMaskOn", true);
            refMouvementController.delayBetweenMovement = delayWhenMasked;

            refRoomsManager.PlayerStatusChanged(isHidden);
        }
    }

    private void Fire_Seed(InputAction.CallbackContext context)
    {
        projectile = Instantiate(seedPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Seed>().moveDir = refMouvementController.lastDir;
    }
}
