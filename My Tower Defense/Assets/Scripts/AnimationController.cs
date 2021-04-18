using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Transform[] states;
    private GameObject[] enemies;
    private bool facingright = false;
    private float rangex = 2f, rangey = 2.5f, attackSpeed = 1f, attackCountdown = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        states = new Transform[transform.childCount];
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length > 0)
        {
            findEnemy();
        }
        else
        {
            states[0].gameObject.SetActive(false);
            states[1].gameObject.SetActive(true);
        }
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
    }

    void findEnemy()
    {
        Transform closestEnemy = GetClosestEnemy(enemies);
        ZombieEnemyController damage = closestEnemy.gameObject.GetComponent<ZombieEnemyController>();
        Vector3 distanceToNearestEnemy = closestEnemy.position - transform.position;
        if (Mathf.Abs(distanceToNearestEnemy.x) <= rangex && Mathf.Abs(distanceToNearestEnemy.y) <= rangey)
        {
            states[1].gameObject.SetActive(false);
            states[0].gameObject.SetActive(true);
            if (damage != null && attackCountdown <= 0)
            {
                damage.TakeDamage(10);
                attackCountdown = 1 / attackSpeed;
            }
        }
        else
        {
            states[0].gameObject.SetActive(false);
            states[1].gameObject.SetActive(true);
        }
        if (distanceToNearestEnemy.x > 0 && !facingright)
        {
            gameObject.transform.Rotate(Vector2.up * 180);
            facingright = true;
        }
        else if (distanceToNearestEnemy.x < 0 && facingright)
        {
            facingright = false;
            gameObject.transform.Rotate(Vector2.up * 180);
        }
    }

    Transform GetClosestEnemy (GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
     
        return bestTarget;
    }
}
