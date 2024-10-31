using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float minPauseTime = 1f;
    public float maxPauseTime = 3f;
    public float minMoveTime = 1f;
    public float maxMoveTime = 4f;

    private float moveSpeed;
    private float moveDuration;
    private Vector2 moveDirection;
    private bool isMoving = false;

    private float screenLeftLimit;
    private float screenRightLimit;

    private Rigidbody2D rb;
    private Transform enemyTransform;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level 2")
        {
            Debug.Log("Level 2");
            minSpeed *= 1.8f;
            maxSpeed *= 1.8f;
            maxPauseTime *= .6f;
        }

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            Debug.Log("Level 3");
            minSpeed *= 2f;
            maxSpeed *= 2f;
            maxPauseTime *= .5f;
        }

        if (SceneManager.GetActiveScene().name == "Level 4")
        {
            Debug.Log("Level 4");
            minSpeed *= 2.2f;
            maxSpeed *= 2.2f;
            maxPauseTime *= .6f;
        }

        rb = GetComponent<Rigidbody2D>();
        enemyTransform = transform;

        // Screen Border
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        screenLeftLimit = leftBoundary.x;
        screenRightLimit = rightBoundary.x;

        StartCoroutine(MoveEnemy());
    }

    IEnumerator MoveEnemy()
    {
        while (true)
        {
            moveSpeed = Random.Range(minSpeed, maxSpeed);
            moveDuration = Random.Range(minMoveTime, maxMoveTime);

            float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            float directionX = Random.Range(-1f, 1f) >= 0 ? 1f : -1f;

            if ((enemyTransform.position.x <= screenLeft && directionX < 0) ||
                (enemyTransform.position.x >= screenRight && directionX > 0))
            {
                directionX *= -1;
            }

            moveDirection = new Vector2(directionX, 0).normalized;

            if (moveDirection.x < 0)
            {
                enemyTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                enemyTransform.localScale = new Vector3(-1, 1, 1);
            }

            isMoving = true;
            rb.velocity = moveDirection * moveSpeed;
            yield return new WaitForSeconds(moveDuration);

            isMoving = false;
            rb.velocity = Vector2.zero;
            float pauseDuration = Random.Range(minPauseTime, maxPauseTime);
            yield return new WaitForSeconds(pauseDuration);
        }
    }


    void Update()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit);
            transform.position = clampedPosition;
        }
    }
}
