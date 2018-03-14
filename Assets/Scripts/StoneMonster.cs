using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class StoneMonster : MonoBehaviour
{

    private Vector3 moveDirection;
    private Vector3 endDirection;
    public float speed = 6.0F;
    private bool goRight = true;
    private bool doPatrolCoroutine = true;
    public bool isDead = false;

    public int nbAnimDeath = 0;

    Animation anim;
    public const string IDLE = "Anim_Idle";
    public const string RUN = "Anim_Run";
    public const string ATTACK = "Anim_Attack";
    public const string DEATH = "Anim_Death";

    public void Start()
    {
        moveDirection = transform.position;
        endDirection = new Vector3(moveDirection.x + 10, moveDirection.y, moveDirection.z);
        anim = GetComponent<Animation>();
        isDead = false;
        nbAnimDeath = 0;
    }

    void Update()
    {
        if (!isDead)
        {
            anim.CrossFade(RUN);

            if (doPatrolCoroutine)
            {
                StartCoroutine("MonsterPatrol");
            }
            else
            {
                StopCoroutine("MonsterPatrol");
                anim.CrossFade(ATTACK);
            }
        } else
        {
            StartCoroutine("MonsterDeath");
        }

    }

    IEnumerator MonsterPatrol()
    {
        if (goRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endDirection, speed * Time.deltaTime);

            if (transform.position == endDirection)
            {
                Quaternion target = Quaternion.Euler(0, -90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0F);

                yield return new WaitForSeconds(1);

                goRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);

            if (transform.position == moveDirection)
            {
                Quaternion target = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0F);

                yield return new WaitForSeconds(1);

                goRight = true;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            doPatrolCoroutine = false;
        }

        if (collider.gameObject.tag == "ElectricBall")
        {
            StopAllCoroutines();
            isDead = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        doPatrolCoroutine = true;
    }

    IEnumerator MonsterDeath()
    {

        if (nbAnimDeath == 0)
        {
            anim.CrossFade(DEATH);
            nbAnimDeath++;
        }
        
        yield return new WaitForSeconds(1);

        //DestroyObject(gameObject);

        StopAllCoroutines();
    }

    public void Reset()
    {
        transform.position = new Vector3(51, -8, 0);
        this.Start();
    }

}