using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] private float delayBetweenMovement;
    [SerializeField] private float boxOffset;
    private float colliderLength = 0.65f;  // 0.976 0.535
    private readonly float pixelDisplacement_x = 0.05f;
    private readonly float pixelDisplacement_y = 0.05f;
    private float movementTimer = 0f;

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
    }

    private void Update()
    {
        movementTimer -= movementTimer <= 0 ? 0 : Time.deltaTime;
        List<Collider2D> wallsHit = new ();
        contactFilter.SetLayerMask(LayerMask.GetMask("Wall"));
        contactFilter.useLayerMask = true;
        float movement_x = transform.position.x + Input.GetAxisRaw("Horizontal") * pixelDisplacement_x;
        float movement_y = transform.position.y + Input.GetAxisRaw("Vertical") * pixelDisplacement_y;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if(movementTimer <= 0f)
            {
                //First check on top of the player
                Physics2D.OverlapCollider(collider_top, contactFilter, wallsHit);
                if(wallsHit.Count != 0 && Input.GetAxisRaw("Vertical") > 0)
                {
                    movement_y = transform.position.y;
                }

                //Second check to the right of the player
                Physics2D.OverlapCollider(collider_right, contactFilter, wallsHit);
                if(wallsHit.Count != 0 && Input.GetAxisRaw("Horizontal") > 0)
                {
                    movement_x = transform.position.x;
                }

                //Third check to the left of the player
                Physics2D.OverlapCollider(collider_left, contactFilter, wallsHit);
                if (wallsHit.Count != 0 && Input.GetAxisRaw("Horizontal") < 0)
                {
                    movement_x = transform.position.x;
                }

                //Forth check underneath the player
                Physics2D.OverlapCollider(collider_down, contactFilter, wallsHit);
                if (wallsHit.Count != 0 && Input.GetAxisRaw("Vertical") < 0)
                {
                    movement_y = transform.position.y;
                }

                if(Input.GetAxisRaw("Horizontal") != 0)
                {
                    movement_y = transform.position.y;
                }

                transform.position = new Vector3(movement_x, movement_y, 0);
                movementTimer = delayBetweenMovement;
            }
        }
    }
}
