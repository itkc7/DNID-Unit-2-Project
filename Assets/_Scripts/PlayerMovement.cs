using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Für den Zugriff auf UI-Elemente

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private float moveInput;
    private bool isJumping = false;

    private Rigidbody2D rb;
    private float screenLeftLimit;
    private float screenRightLimit;

    // Referenz zum UI-Image (Game Over)
    public GameObject gameOverImage;
    public GameObject gameWonImage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Bildschirmränder basierend auf der Kamera berechnen
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        screenLeftLimit = leftBoundary.x;
        screenRightLimit = rightBoundary.x;

        // UI-Image am Anfang ausblenden
        gameOverImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontale Bewegung
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Begrenzen der X-Position innerhalb der Bildschirmgrenzen
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit);
        transform.position = clampedPosition;

        // Springen
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-0.4256694f, 0.4256694f, 0.4256694f); // Dreht den Enemy nach links
        }
        else
        {
            transform.localScale = new Vector3(0.4256694f, 0.4256694f, 0.4256694f);  // Dreht den Enemy nach rechts
        }
    }

    // Auf dem Boden landen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        // Prüfen, ob der Spieler den Enemy berührt
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");
            gameOverImage.SetActive(true);
        }

        //if (collision.gameObject.CompareTag("Item"))
        //{
        //    gameWonImage.SetActive(true);  // Das UI-Element anzeigen
        //}
    }

   /* private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            gameWonImage.SetActive(true);  // Das UI-Element anzeigen
        }
    }*/
}
