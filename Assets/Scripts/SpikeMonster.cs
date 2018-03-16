using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpikeMonster : MonoBehaviour
{
    public LayerMask aggroLayerMask;
    public float speed = 6.0F;
    
    private Vector3 moveDirection;
    private Vector3 endDirection;
    private bool goUp = true;
    private float idleWaitingTime = 0.1f;
    private Collider[] withinAggroColliders;

    // Use this for initialization
    void Start()
    {
        moveDirection = transform.position;
        endDirection = new Vector3(moveDirection.x, moveDirection.y + 1, moveDirection.z);
        StartCoroutine("SpikeIDLE");
    }

    // Update is called once per frame
    void Update()
    {
        
        withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask);

        if (withinAggroColliders.Length > 0)
        {
            StopCoroutine("SpikeIDLE");
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        } else
        {
            StartCoroutine("SpikeIDLE");
        }
    }

    void ChasePlayer(Player player)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ElectricBall")
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator SpikeIDLE()
    {
        if (goUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endDirection, speed * Time.deltaTime);

            if (transform.position == endDirection)
            {
                yield return new WaitForSeconds(idleWaitingTime);
                goUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);

            if (transform.position == moveDirection)
            {
                yield return new WaitForSeconds(idleWaitingTime);
                goUp = true;
            }
        }
    }

}