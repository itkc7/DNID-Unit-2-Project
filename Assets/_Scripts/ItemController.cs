using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-8f, -4f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8f, 4f);

    [Header("Collection Settings")]
    [SerializeField] private int requiredItems = 3;

    private static int collectedItems = 0;
    private static bool isGameComplete = false;
    private bool isCollected = false;

    public delegate void OnItemCollectedDelegate(int currentCount, int required);
    public static event OnItemCollectedDelegate OnItemCollected;

    void Start()
    {
        // Reset static variables if this is the first item instance
        if (GameObject.FindObjectsOfType<ItemController>().Length == 1)
        {
            collectedItems = 0;
            isGameComplete = false;
        }

        // Ensure the item starts active but in a random position
        RepositionItem();
        gameObject.SetActive(true);

        // Start spawning routine for subsequent positions
        StartCoroutine(SpawnRoutine());

        // Debug log to verify the script is running
        Debug.Log("ItemController started on: " + gameObject.name);
    }

    private void RepositionItem()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        transform.position = randomPosition;
        Debug.Log($"Item repositioned to: {randomPosition}"); // Debug log
    }

    private IEnumerator SpawnRoutine()
    {
        Debug.Log("SpawnRoutine started"); // Debug log

        while (!isGameComplete && !isCollected)
        {
            // Wait for random delay
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            if (!isCollected && !isGameComplete)
            {
                Debug.Log("Attempting to spawn item"); // Debug log
                RepositionItem();
                gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name}"); // Debug log

        if (other.CompareTag("Player") && !isCollected && !isGameComplete)
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        isCollected = true;
        gameObject.SetActive(false);

        collectedItems++;
        OnItemCollected?.Invoke(collectedItems, requiredItems);

        Debug.Log($"Item collected! Total: {collectedItems}/{requiredItems}"); // Debug log

        if (collectedItems >= requiredItems)
        {
            isGameComplete = true;
            Debug.Log("All items collected! Level complete!");
        }
    }

}