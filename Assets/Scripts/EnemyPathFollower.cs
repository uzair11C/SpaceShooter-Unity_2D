using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public List<Transform> pathPoints; // Assigned by spawner
    private int currentPointIndex = 0;

    void Update()
    {
        if (pathPoints == null || pathPoints.Count == 0)
            return;

        Transform targetPoint = pathPoints[currentPointIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            2f * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
        }
    }
}
