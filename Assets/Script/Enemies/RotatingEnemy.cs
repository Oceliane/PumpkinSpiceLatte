using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] detectionAreas;
    [SerializeField] LayerMask layerMask;

    [Header("Settings")]
    [SerializeField] bool rotationAntiHoraire;
    [SerializeField] float delayBetweenRotations;
    float delay;

    private void Start()
    {
        delay = delayBetweenRotations;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetection();

        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            if (transform.eulerAngles.z >= 360 || transform.eulerAngles.z <= -360)
                transform.eulerAngles = Vector3.zero;

            if (rotationAntiHoraire)
                transform.eulerAngles += new Vector3(0, 0, 90);
            else
                transform.eulerAngles -= new Vector3(0, 0, 90);

            delay = delayBetweenRotations;
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
}
