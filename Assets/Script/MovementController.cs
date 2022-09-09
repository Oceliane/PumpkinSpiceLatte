using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [HideInInspector] public bool isHelmetOnHead;

    [Header("Collider settings")]
    [SerializeField] private float boxOffset;
    [SerializeField] private float colliderLength = 0.65f; // 0.976 0.535

    [Header("Movement settings")]
    [SerializeField] private float pixelDisplacement_x = 0.05f;
    [SerializeField] private float pixelDisplacement_y = 0.05f;
    public float delayBetweenMovement;

    [Header("Helmet settings")]
    [SerializeField] private float helmetTimer;
    [SerializeField] private float helmetCooldown;
    [SerializeField] private float helmetRecoveryRatio;
    
    [SerializeField] Animator playerAnimator;
    private float movementTimer = 0f;
    public Vector3 lastDir = Vector3.right;

    public PlayerInputAction playerControls;
    private float helmetTimerIntern;
    private float helmetDelayTimer;

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

        collider_top = gameObject.AddComponent<BoxCollider2D>();
        collider_top.size = new Vector2(colliderLength, 0.05f);
        collider_top.offset = new Vector2(0f, boxOffset);

        playerControls = InputManager.inputActions;
        playerControls.Player.Enable();
        playerControls.Player.Helmet.started += Action_Helmet;
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
            helmetTimerIntern -= helmetTimerIntern <= 0 ? 0 : Time.deltaTime;
        else
            helmetTimerIntern += helmetTimerIntern >= helmetTimer ? 0 : Time.deltaTime * helmetRecoveryRatio;
        helmetDelayTimer -= helmetDelayTimer <= 0 ? 0 : Time.deltaTime;

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

                lastDir = playerControls.Player.Move.ReadValue<Vector2>();
                playerAnimator.SetBool("IsWalking", true);
            }
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Collision trigger with " + c.gameObject.name);
        Door d;
        if (c.gameObject.TryGetComponent<Door>(out d))
        {
            d.GoNextRoom();
        }
    }

    private void Action_Helmet(InputAction.CallbackContext context)
    {
        if(helmetDelayTimer <= 0)
        {
            if (isHelmetOnHead)
            {
                isHelmetOnHead = false;
                Debug.Log("Helmet off");
            }
            else
            {
                isHelmetOnHead = true;
                Debug.Log("Helmet on");
            }
            helmetDelayTimer = helmetCooldown;
        }
    }
}
