using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 2f;
    private Transform target;
    private int pointIndex = 0;
    // Start is called before the first frame update

    void Start()
    {
        target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= 0.25f)
        {
            GetToNextPoint();
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
        target = Waypoints.points[pointIndex];
    }
}
