using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
   //public Transform Player;
    NavMeshAgent agent;
    public GameObject healthBar;
    public float attackRaduis=5;
    Animator anim;
    bool caAttack = true;
    float attackCooldown = 3f;
    CharStates stats;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharStates>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(transform.position, LevelManager.instance.player.position);
       
        if (distance < attackRaduis)
        {
            agent.SetDestination(LevelManager.instance.player.position);
            if(distance<=agent.stoppingDistance)
            {
                if(caAttack)
                {
                    StartCoroutine(cooldown());
                    anim.SetTrigger("attack");
                }
            }
        }
    }
    
    IEnumerator cooldown()
    {
        caAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        caAttack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            //Debug.Log("Player Contacted!");
            stats.ChangeHealth(-other.GetComponentInParent<CharStates>().power);
        //Destroy(gameObject);
    }
    public void DamagePlayer()
    {
        LevelManager.instance.player.GetComponent<CharStates>().ChangeHealth(-stats.power);
    }
    public void  EnemyDie()
    {
        Destroy(agent);
        anim.SetTrigger("dead");
        healthBar.SetActive(false);
    }
}
