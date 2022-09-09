using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public Vector3 distance;
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        VICTORY
    }
    public Direction DoorDirection;
    public void GoNextRoom()
    {
        RoomsManager.instance.GoToNextRoom(DoorDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(distance, 0.1f);
    }
}
