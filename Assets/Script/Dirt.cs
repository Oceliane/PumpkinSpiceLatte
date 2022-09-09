using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] GameObject pumpkin;
    [SerializeField] SpriteRenderer refRend;
    [SerializeField] BoxCollider2D refCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(pumpkin, transform);
        refRend.enabled = false;
        refCollider.enabled = false;
    }
}
