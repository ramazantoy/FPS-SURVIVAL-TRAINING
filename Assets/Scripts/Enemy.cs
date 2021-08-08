using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent fallow;
    [SerializeField]
    Transform aim;
    Animator a;
    float hp = 100;
    bool attack = false;
    Coroutine hp_down;
    Manager m;
    float distance;
    void Start()
    {
        fallow = GetComponent<NavMeshAgent>();
        m = GameObject.Find("manager").GetComponent<Manager>();
        a = GetComponent<Animator>();


    }
    void Update()
    {
        if (aim != null)
        {
            fallow.SetDestination(aim.position);
            if (hp > 0.0f)
            {
                distance = Vector3.Distance(transform.position, aim.transform.position);
                if (distance<= 2.0f)
                {
                    a.SetTrigger("attack");
                    if (attack == false)
                    {
                        attack = true;
                        hp_down = StartCoroutine(playerHpDown(-5.0f));
                    }
                }
                else if (distance> 2.0f)
                {
                    a.SetBool("run", true);
                    attack = false;

                }

                //Debug.Log(fallow.remainingDistance);
            }
           else if (hp <= 0.0f)
            {
                death();
            }
        }
        
    }
    IEnumerator playerHpDown(float h)
    {
        if (attack == true)
        {
            aim.GetComponent<Health>().healtChange(h);
            yield return new WaitForSeconds(1.0f);
            yield return playerHpDown(h);
        }
        else
        {
            StopCoroutine(hp_down);
        }

    }
    void death()
    {
        a.SetTrigger("die");
        check();
        Destroy(fallow);
        Destroy(this);
        Destroy(gameObject, 2.0f);
    }
    void check()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
        if (enemys.Length <= 1)
        {
            m.wellDone();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            other.gameObject.SetActive(false);
            hpDown(-20.0f);
        }
    }
    void hpDown(float h)
    {
        hp =hp+h;
        
        if (hp <= 0.0f)
        {
            death();
        }
    }
}
