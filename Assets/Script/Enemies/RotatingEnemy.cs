using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : Enemy
{
    [SerializeField] GameObject[] detectionAreas;
    [SerializeField] Animator gargouilleAnim;
    [SerializeField] Sprite[] gargouilleSprites;
    [SerializeField] LayerMask layerMask;

    [Header("Settings")]
    [SerializeField] bool rotationAntiHoraire;
    [SerializeField] float delayBetweenRotations;
    float delay;
    [SerializeField] float state;

    private void Start()
    {
        delay = delayBetweenRotations;

        if (state == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            gargouilleAnim.SetTrigger("Right");
        }
        else if (state == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            gargouilleAnim.SetTrigger("Up");
        }
        else if (state == 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            gargouilleAnim.SetTrigger("Left");
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            gargouilleAnim.SetTrigger("Down");
        }

        foreach (var item in detectionAreas)
        {
            item.transform.localEulerAngles = transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            if (rotationAntiHoraire)
            {
                state++;

                if (state >= gargouilleSprites.Length)
                    state = 0;
            }
            else
            {
                state--; 

                if (state < 0)
                    state = gargouilleSprites.Length;
            }

            if (state == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                gargouilleAnim.SetTrigger("Right");
            }
            else if (state == 1)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                gargouilleAnim.SetTrigger("Up");
            }
            else if (state == 2)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
                gargouilleAnim.SetTrigger("Left");
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
                gargouilleAnim.SetTrigger("Down");
            }

            foreach (var item in detectionAreas)
            {
                item.transform.localEulerAngles = transform.eulerAngles;
            }

            delay = delayBetweenRotations;
        }

        EnemyDetection();
    }

    void EnemyDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 2.4f, ~layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 2.4f, Color.red);


        if (hit && !playerIsHidden)
        {
            Debug.Log(hit.collider.name);
            if (hit.distance < 0.8)
            {
                detectionAreas[0].SetActive(true);
                detectionAreas[1].SetActive(false);
                detectionAreas[2].SetActive(false);
            }
            else if (hit.distance < 1.6)
            {
                detectionAreas[0].SetActive(true);
                detectionAreas[1].SetActive(true);
                detectionAreas[2].SetActive(false);
            }
            else
            {
                detectionAreas[0].SetActive(true);
                detectionAreas[1].SetActive(true);
                detectionAreas[2].SetActive(true);
            }
        }
        else if (!detectionAreas[2].activeInHierarchy && !playerIsHidden)
        {
            detectionAreas[0].SetActive(true);
            detectionAreas[1].SetActive(true);
            detectionAreas[2].SetActive(true);
        }
    }

    public override void BlindEnemy()
    {
        playerIsHidden = true;

        detectionAreas[1].SetActive(false);
        detectionAreas[2].SetActive(false);
    }

    public override void EnemyCanSee()
    {
        playerIsHidden = false;

        foreach (var item in detectionAreas)
        {
            item.SetActive(true);
        }

        EnemyDetection();
    }
}
