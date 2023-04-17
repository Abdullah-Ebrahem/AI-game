using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public CharMovemets CharMove;
    public void PlayerAttack()
    {
        Debug.Log("Player Attacked!");
        CharMove.DoAttack();
    }
    public void PlayerDamage()
    {
        transform.GetComponentInParent<EnemyController>().DamagePlayer();
    }
    
}
