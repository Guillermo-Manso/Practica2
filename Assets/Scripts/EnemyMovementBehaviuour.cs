using UnityEngine;

public class EnemyMovementBehaviuour : MonoBehaviour
{
    public Transform[] patrolPoints; // puntos de patrulla
    public float speed = 2.0f; // Velocidad de movimiento 
    private int currentPointIndex = 0; // patrulla actual

    public float rotationSpeed = 5.0f;

    private float reachThreshold = 0.3f; // Umbral para cambiar al siguiente punto

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            // primer punto de patrulla al inicio
            transform.position = patrolPoints[0].position;
        }
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        // Mover hacia el punto de patrulla actual
        Transform targetPoint = patrolPoints[currentPointIndex];

        // Hacer que el objeto mire hacia el siguiente punto de patrulla
        Vector3 direction = targetPoint.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Revisar si el enemigo ha alcanzado el punto de patrulla actual
        if (Vector3.Distance(transform.position, targetPoint.position) < reachThreshold)
        {
            // Cambiar al siguiente punto de patrulla
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}
