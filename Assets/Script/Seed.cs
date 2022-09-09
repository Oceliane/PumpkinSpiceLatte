using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [HideInInspector] public Vector3 moveDir;
    public float delayBetweenMovements;
    float delay;

    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            transform.position += moveDir * 0.05f;

            delay = delayBetweenMovements;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            Destroy(gameObject);
    }
}
