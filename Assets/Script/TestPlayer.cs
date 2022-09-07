using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TestPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D c)
    {
        Door d;
        Debug.Log("Collision with " + c.gameObject.name);
        if (c.gameObject.TryGetComponent<Door>(out d))
        {

            d.GoNextRoom();
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Collision trigger with " + c.gameObject.name);
        Door d;
        if(c.gameObject.TryGetComponent<Door>(out d))
        {
            d.GoNextRoom();
        }
    }
}
