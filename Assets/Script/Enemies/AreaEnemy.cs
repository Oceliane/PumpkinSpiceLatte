using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Enemy
{
    [SerializeField] GameObject[] detectionAreas;

    public override void BlindEnemy()
    {
        foreach (var item in detectionAreas)
        {
            item.SetActive(false);
        }
    }

    public override void EnemyCanSee()
    {
        foreach (var item in detectionAreas)
        {
            item.SetActive(true);
        }
    }
}
