using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPositions;
    [SerializeField] int startingPos;
    int currentPos;
    int targetPos;
    [SerializeField] bool loops;
    [SerializeField] float moveSpeed;
    [SerializeField] Color lineColor;

    private void Start()
    {
        startingPos = Random.Range(0, patrolPositions.Length);
        transform.position = patrolPositions[startingPos].position;
        currentPos = startingPos;
    }

    void Update()
    {
        Vector3 moveDir = (transform.position - patrolPositions[currentPos].position).normalized;
        transform.position += moveDir * moveSpeed;
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
