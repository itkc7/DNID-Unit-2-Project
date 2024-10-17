using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float minSpeed = 2f;  // Minimale Geschwindigkeit des Enemies
    public float maxSpeed = 5f;  // Maximale Geschwindigkeit des Enemies
    public float minPauseTime = 1f;  // Minimale Zeit, die der Enemy steht
    public float maxPauseTime = 3f;  // Maximale Zeit, die der Enemy steht
    public float minMoveTime = 1f;   // Minimale Zeit der Bewegung
    public float maxMoveTime = 4f;   // Maximale Zeit der Bewegung

    private float moveSpeed;         // Aktuelle Geschwindigkeit
    private float moveDuration;      // Wie lange der Enemy sich bewegt
    private Vector2 moveDirection;   // Bewegungsrichtung (auf X-Achse beschränkt)
    private bool isMoving = false;   // Ob der Enemy sich bewegt oder pausiert

    private float screenLeftLimit;
    private float screenRightLimit;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Bildschirmgrenzen berechnen
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        screenLeftLimit = leftBoundary.x;
        screenRightLimit = rightBoundary.x;

        // Start der Bewegungscoroutine
        StartCoroutine(MoveEnemy());
    }

    // Coroutine für die zufällige Bewegung des Enemies
    IEnumerator MoveEnemy()
    {
        while (true)
        {
            // Zufällige Geschwindigkeit und Bewegungsdauer
            moveSpeed = Random.Range(minSpeed, maxSpeed);
            moveDuration = Random.Range(minMoveTime, maxMoveTime);

            // Zufällige Bewegungsrichtung auf der X-Achse (-1 für links, 1 für rechts)
            float directionX = Random.Range(-1f, 1f) >= 0 ? 1f : -1f;
            moveDirection = new Vector2(directionX, 0).normalized; // Y bleibt 0, nur X bewegt sich

            isMoving = true;
            yield return new WaitForSeconds(moveDuration);

            // Stoppe die Bewegung und warte eine zufällige Pause
            isMoving = false;
            rb.velocity = Vector2.zero;
            float pauseDuration = Random.Range(minPauseTime, maxPauseTime);
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Bewege den Enemy nur entlang der X-Achse
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

            // Begrenze die Position des Enemies innerhalb der Bildschirmgrenzen auf der X-Achse
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit);
            transform.position = clampedPosition;
        }
    }
}
