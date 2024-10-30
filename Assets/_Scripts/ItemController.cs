using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 5f;

    public int requiredItems = 3;

    public int collectedItems = 0;
    private static bool isGameComplete = false;

    public GameObject gameWonImage;

    public ItemCollectUI ui;

    public PlatformManager platformManager;

    public AudioSource collectSound;

    void Start()
    {
        Time.timeScale = 1f;
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(RepositionItem());

    }

    private IEnumerator RepositionItem()
    {

        float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(waitTime);

        Vector2 spawnPosition = platformManager.GetRandomActivePlatformPosition();
        Debug.Log(spawnPosition);
        if (spawnPosition != Vector2.zero)
        {
            spawnPosition.y += 0.3f;
            transform.position = spawnPosition;
        }
        GetComponent<Renderer>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectItem();
            ui.updateUI();
        }
    }

    private void CollectItem()
    {

        collectedItems++;

        if (collectSound != null)
        {
            collectSound.Play();
        }

        Debug.Log($"Item collected! Total: {collectedItems}/{requiredItems}");
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(RepositionItem());




        if (collectedItems >= requiredItems)
        {
            isGameComplete = true;
            Debug.Log("All items collected! Level complete!");
            gameWonImage.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}