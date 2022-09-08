using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public PlayerInputAction playerControls;

    [SerializeField] private float delayBetweenMovement;
    [SerializeField] private float boxOffset;
    [SerializeField] private float colliderLength = 0.976f;
    [SerializeField] private float pixelDisplacement_x = 0.05f;
    [SerializeField] private float pixelDisplacement_y = 0.05f;
    [SerializeField] private float helmetTimer;
    
    private float movementTimer = 0f;

    private float helmetTimerIntern;
    private bool isHelmetOnHead;

    private BoxCollider2D collider_right;
    private BoxCollider2D collider_left;
    private BoxCollider2D collider_top;
    private BoxCollider2D collider_down;

    private ContactFilter2D contactFilter;

    private void Awake()
    {
        collider_right = gameObject.AddComponent<BoxCollider2D>();
        collider_right.size = new Vector2(0.05f, colliderLength);
        collider_right.offset = new Vector2(boxOffset, 0f);

        collider_down = gameObject.AddComponent<BoxCollider2D>();
        collider_down.size = new Vector2(colliderLength, 0.05f);
        collider_down.offset = new Vector2(0f, -boxOffset);

        collider_left = gameObject.AddComponent<BoxCollider2D>();
        collider_left.size = new Vector2(0.05f, colliderLength);
        collider_left.offset = new Vector2(-boxOffset, 0f);

        collider_top = gameObject.AddComponent<BoxCollider2D>();
        collider_top.size = new Vector2(colliderLength, 0.05f);
        collider_top.offset = new Vector2(0f, boxOffset);

        playerControls = new PlayerInputAction();
        playerControls.Player.Enable();
    }


    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        //Timer handling
        movementTimer -= movementTimer <= 0 ? 0 : Time.deltaTime;
        if (isHelmetOnHead)
            helmetTimerIntern -= Time.deltaTime;

        //Filtering setup for colliders
        List<Collider2D> wallsHit = new ();
        contactFilter.SetLayerMask(LayerMask.GetMask("Wall"));
        contactFilter.useLayerMask = true;

        Vector2 input = playerControls.Player.Move.ReadValue<Vector2>();
        float movement_x = transform.position.x + input.x * pixelDisplacement_x;
        float movement_y = transform.position.y + input.y * pixelDisplacement_y;

        if (input != Vector2.zero)
        {
            if(movementTimer <= 0f)
            {
                //First check on top of the player
                Physics2D.OverlapCollider(collider_top, contactFilter, wallsHit);
                if(wallsHit.Count != 0 && input.y > 0)
                {
                    movement_y = transform.position.y;
                }

                //Second check to the right of the player
                Physics2D.OverlapCollider(collider_right, contactFilter, wallsHit);
                if(wallsHit.Count != 0 && input.x > 0)
                {
                    movement_x = transform.position.x;
                }

                //Third check to the left of the player
                Physics2D.OverlapCollider(collider_left, contactFilter, wallsHit);
                if (wallsHit.Count != 0 && input.x < 0)
                {
                    movement_x = transform.position.x;
                }

                //Forth check underneath the player
                Physics2D.OverlapCollider(collider_down, contactFilter, wallsHit);
                if (wallsHit.Count != 0 && input.y < 0)
                {
                    movement_y = transform.position.y;
                }

                //Diagonal cancel
                if(input.x != 0)
                {
                    movement_y = transform.position.y;
                }

                transform.position = new Vector3(movement_x, movement_y, 0);
                movementTimer = delayBetweenMovement;
            }
        }
    }

    private void ActionHelmet()
    {
        if (isHelmetOnHead)
        {
            
        }
        else
        {

        }
    }
}
