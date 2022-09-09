using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemy : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform direction;
    [SerializeField] GameObject[] detectionAreas;

    void Update()
    {
        EnemyDetection();
    }

    void EnemyDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.TransformDirection(Vector2.right), 2.4f, ~layerMask);
        Debug.DrawRay(transform.position, direction.TransformDirection(Vector3.right) * 2.4f, Color.red);


        if (hit)
        {
            //Debug.Log(hit.collider.name);
            if (hit.distance < 0.7)
            {
                detectionAreas[0].SetActive(false);
                detectionAreas[1].SetActive(false);
                detectionAreas[2].SetActive(false);
            }
            else if (hit.distance < 1.2)
            {
                detectionAreas[0].SetActive(true);
                detectionAreas[1].SetActive(false);
                detectionAreas[2].SetActive(false);
            }
            else if (hit.distance < 2)
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
}
