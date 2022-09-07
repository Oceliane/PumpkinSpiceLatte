using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPositions;
    [SerializeField] int[] rotations;
    [SerializeField] GameObject[] detectionAreas;
    [SerializeField] Color lineColor;
    [SerializeField] LayerMask layerMask;
    int currentPos;
    int targetPos;
    Vector3 moveDir;
    float nbrMovement;
    bool goBackwards;

    [Header("Settings")]
    [SerializeField] float delayBetweenMovements;
    float delay;
    [SerializeField] bool loops;
    [SerializeField] bool randomStartingPos;
    [SerializeField] int startingPos = 0;

    //un pixel = 0.05

    private void Start()
    {
        if (randomStartingPos)
        {
            startingPos = Random.Range(0, patrolPositions.Length);

        }

        transform.position = patrolPositions[startingPos].position;
        currentPos = startingPos;
        delay = delayBetweenMovements;

        UpdateDirection();
    }

    void Update()
    {
        EnemyDetection();

        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            transform.position += moveDir * 0.05f;
            nbrMovement--;

            if (nbrMovement < 1)
            {
                transform.position = patrolPositions[targetPos].position;
                currentPos = targetPos;

                //if (currentPos >= patrolPositions.Length)
                //    currentPos = 0;

                UpdateDirection();
            }
            
            delay = delayBetweenMovements;
        }
    }

    void EnemyDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 2.4f, ~layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 2.4f, Color.red);


        if (hit)
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
        else
        {
            detectionAreas[0].SetActive(true);
            detectionAreas[1].SetActive(true);
            detectionAreas[2].SetActive(true);
        }
    }

    void UpdateDirection()
    {
        if (loops)
        {
            if (currentPos == patrolPositions.Length - 1)
                targetPos = 0;
            else
                targetPos = currentPos + 1;

            //transform.eulerAngles = new Vector3(0, 0, rotations[targetPos]);
        }   
        else
        {
            if (goBackwards)
            {
                if (currentPos == 0)
                {
                    targetPos = currentPos + 1;
                    goBackwards = !goBackwards;
                }
                else
                    targetPos = currentPos - 1;

                //transform.eulerAngles = new Vector3(0, 0, rotations[targetPos]);
            }
            else
            {
                if (currentPos == patrolPositions.Length - 1)
                {
                    targetPos = currentPos - 1;
                    goBackwards = !goBackwards;
                }
                else
                    targetPos = currentPos + 1;

                //transform.eulerAngles = new Vector3(0, 0, rotations[targetPos]);
            }
        }
        

        Vector3 targetDirection = patrolPositions[targetPos].position - patrolPositions[currentPos].position;
        float distance = Vector3.Distance(patrolPositions[targetPos].position, patrolPositions[currentPos].position);
        nbrMovement = distance / 0.05f;
        moveDir = targetDirection.normalized;

        //transform.rotation = Quaternion.LookRotation(moveDir);
        transform.eulerAngles = new Vector3(0, 0, rotations[targetPos]);
        //transform.LookAt(patrolPositions[targetPos], Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        if (patrolPositions.Length > 1)
        {
            for (int i = 0; i < patrolPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(patrolPositions[i].position, patrolPositions[i + 1].position);
            }
        }    
        
        if (loops)
            Gizmos.DrawLine(patrolPositions[patrolPositions.Length-1].position, patrolPositions[0].position);
    }
}
