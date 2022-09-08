using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]
    public GameObject RoomPrefab;
    public Enemy[] roomEnemies;
    public bool bIsReset;
    [HideInInspector]
    public Vector2 Coords;

    public Transform SpawnLeft;
    public Transform SpawnRight;
    public Transform SpawnUp;
    public Transform SpawnDown;

    public void RoomEnemyChange(bool playerIsHidden)
    {
        if (playerIsHidden)
        {
            foreach (var item in roomEnemies)
            {
                item.BlindEnemy();
            }
        }
        else
        {
            foreach (var item in roomEnemies)
            {
                item.EnemyCanSee();
            }
        }
    }
}
