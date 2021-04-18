using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTurretController : MonoBehaviour
{
    private GameController gameController;
    private GameObject[] enemies;
    private bool facingright = false;
    private Animator animator;
    private float rangex = 2f, rangey = 2.5f, attackSpeed = 5f, attackCountdown = 0f, damage = 10f;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject myCanvas = GameObject.Find("Canvas");
        gameController = myCanvas.GetComponent<GameController>();
        attackCountdown = 1 / attackSpeed;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            findEnemy();
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
    }

    void findEnemy()
    {
        Transform closestEnemy = GetClosestEnemy(enemies);
        ZombieEnemyController attack = closestEnemy.gameObject.GetComponent<ZombieEnemyController>();
        Vector3 distanceToNearestEnemy = closestEnemy.position - transform.position;
        if (Mathf.Abs(distanceToNearestEnemy.x) <= rangex && Mathf.Abs(distanceToNearestEnemy.y) <= rangey)
        {
            animator.SetBool("isAttacking", true);
            if (attack != null && attackCountdown <= 0)
            {
                attack.TakeDamage(damage);
                attackCountdown = 1 / attackSpeed;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
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

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
