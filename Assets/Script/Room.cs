using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]
    public GameObject RoomPrefab;
    public bool bIsReset;
    [HideInInspector]
    public Vector2 Coords;

    public Transform SpawnLeft;
    public Transform SpawnRight;
    public Transform SpawnUp;
    public Transform SpawnDown;
}
