using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class Monster_behaviour : MonoBehaviour
{

    private Vector3 moveDirection;
    private Vector3 endDirection;
    public float speed = 6.0F;
    private bool goRight = true;
    private bool doCoroutine = true;
    private bool isDead = false;

    Animation anim;
    public const string IDLE = "Anim_Idle";
    public const string RUN = "Anim_Run";
    public const string ATTACK = "Anim_Attack";
    public const string DEATH = "Anim_Death";

    // machine à états --> enum sur les états

    private void Start()
    {
        moveDirection = transform.position;
        endDirection = new Vector3(moveDirection.x + 10, moveDirection.y, moveDirection.z);
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        if (!isDead)
        {
            anim.CrossFade(RUN);

            if (doCoroutine)
            {
                StartCoroutine("MonsterPatrol");
            }
            else
            {
                StopCoroutine("MonsterPatrol");
                anim.CrossFade(ATTACK);
            }
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
            doCoroutine = false;
        }

        if (collider.gameObject.tag == "RoseFireBall")
        {
            StopAllCoroutines();
            isDead = true;
            anim.CrossFade(DEATH);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        doCoroutine = true;
    }

}