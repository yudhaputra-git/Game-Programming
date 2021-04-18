using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class ZombieEnemyController : MonoBehaviour
{
    private GameController gameController;
    public int bounty = 100;
    private float speed = 2f;
    private Transform nextPoint;
    private int pointIndex = 0;
    private float health = 100f;
    private Animator animationController;
    // Start is called before the first frame update

    void Start()
    {
        GameObject myCanvas = GameObject.Find("Canvas");
        gameController = myCanvas.GetComponent<GameController>();
        animationController = gameObject.GetComponent<Animator>();
        if(Waypoints.points != null)
        {
            nextPoint = Waypoints.points[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health > 0 && nextPoint != null)
        {
            Vector2 direction = nextPoint.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPoint.position) <= 0.25f)
            {
                GetToNextPoint();
            }
        }
        else
        {
            speed = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            animationController.SetBool("isDead", true);
            gameController.GetGold(bounty);
            Destroy(gameObject);
        }
    }

    void GetToNextPoint()
    {
        if (pointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        pointIndex++;
        nextPoint = Waypoints.points[pointIndex];
    }
}
