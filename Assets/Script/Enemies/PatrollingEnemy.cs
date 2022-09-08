using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPositions;
    [SerializeField] GameObject[] detectionAreas;
    [SerializeField] GameObject objectToRotate;
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

                UpdateDirection();
            }
            
            delay = delayBetweenMovements;
        }
    }

    void EnemyDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, objectToRotate.transform.TransformDirection(Vector2.right), 2.4f, ~layerMask);
        //Debug.DrawRay(transform.position, objectToRotate.transform.TransformDirection(Vector3.right) * 2.4f, Color.red);


        if (hit)
        {
            //Debug.Log(hit.collider.name);
            if (hit.distance < 1.2)
            {
                detectionAreas[0].SetActive(true);
                detectionAreas[1].SetActive(false);
                detectionAreas[2].SetActive(false);
            }
            else if (hit.distance < 2.2)
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
        else if (!detectionAreas[2].activeInHierarchy)
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
                {
                    targetPos = currentPos - 1;
                }
            }
            else
            {
                if (currentPos == patrolPositions.Length - 1)
                {
                    targetPos = currentPos - 1;
                    goBackwards = !goBackwards;
                }
                else
                {
                    targetPos = currentPos + 1;
                }
            }
        }
        

        Vector3 targetDirection = patrolPositions[targetPos].position - patrolPositions[currentPos].position;
        float distance = Vector3.Distance(patrolPositions[targetPos].position, patrolPositions[currentPos].position);
        nbrMovement = distance / 0.05f;
        moveDir = targetDirection.normalized;

        if (moveDir.x < 0)
        {
            objectToRotate.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (moveDir.x > 0)
        {
            objectToRotate.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveDir.y < 0)
        {
            objectToRotate.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (moveDir.y > 0)
        {
            objectToRotate.transform.eulerAngles = new Vector3(0, 0, 90);
        }


        foreach (var item in detectionAreas)
        {
            item.transform.localEulerAngles = objectToRotate.transform.eulerAngles;
        }
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
