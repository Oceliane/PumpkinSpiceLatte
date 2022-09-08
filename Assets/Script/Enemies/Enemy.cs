using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool playerIsHidden;

    public virtual void BlindEnemy()
    {
        playerIsHidden = true;
    }

    public virtual void EnemyCanSee()
    {
        playerIsHidden = false;
    }
}
