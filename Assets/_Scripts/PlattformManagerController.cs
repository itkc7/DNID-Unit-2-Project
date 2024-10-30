using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platforms;
    private List<GameObject> activePlatforms = new List<GameObject>();

    public int numberOfActivePlatforms = 2;

    public float spawnRadius = 3f;

    public float platformCooldown = 1.3f;
    private Dictionary<GameObject, float> cooldownTimers = new Dictionary<GameObject, float>();

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level 2")
        {
            platformCooldown *= .8f;
        }
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            platformCooldown *= .65f;
        }
        if (SceneManager.GetActiveScene().name == "Level 4")
        {
            platformCooldown *= .45f;
        }

        InitializeActivePlatforms();
    }

    private void InitializeActivePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform.activeSelf) 
            {
                activePlatforms.Add(platform);
            }
            if (!cooldownTimers.ContainsKey(platform))
            {
                cooldownTimers[platform] = 0f;
            }
        }
    }

    public void PlatformTriggered(GameObject currentPlatform)
    {
        if (platforms.Length == 0)
        {
            Debug.LogError("No platforms defined in the manager!");
            return;
        }

        if (Time.time < cooldownTimers[currentPlatform])
        {
            Debug.Log("Platform on cooldown.");
            return;
        }

        cooldownTimers[currentPlatform] = Time.time + platformCooldown;
        StartCoroutine(DelayAndShake(currentPlatform));
    }

    private IEnumerator DelayAndShake(GameObject platform)
    {
        yield return new WaitForSeconds(platformCooldown);

        Vector3 originalPosition = platform.transform.position;
        float shakeDuration = 0.8f;
        float shakeMagnitude = 0.1f;

        while (shakeDuration > 0)
        {
            platform.transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        platform.transform.position = originalPosition;
        platform.SetActive(false);
        activePlatforms.Remove(platform);

        ActivateRandomPlatform(platform, platform.transform.position);
    }

    private void ActivateRandomPlatform(GameObject currentPlatform, Vector3 targetPosition)
    {
        Debug.Log("ActivateRandomPlatform");

        GameObject newPlatform;
        int attempts = 0; // Sicherheitsschleife, um eine Endlosschleife zu vermeiden
        do
        {
            newPlatform = platforms[Random.Range(0, platforms.Length)];
            attempts++;
        }
        while ((newPlatform == currentPlatform || activePlatforms.Contains(newPlatform) ||
               Vector3.Distance(newPlatform.transform.position, targetPosition) > spawnRadius)
               && attempts < 15);

        if (attempts >= 10)
        {
            Debug.LogWarning("Could not find a new platform to activate within the radius!");
            return;
        }

        activePlatforms.Add(newPlatform);
        newPlatform.SetActive(true);

        // Setze Cooldown zurück
        cooldownTimers[newPlatform] = Time.time + platformCooldown;
    }

    public Vector2 GetRandomActivePlatformPosition()
    {
        if (activePlatforms.Count == 0)
        {
            Debug.LogWarning("No active platforms available for item spawn.");
            return Vector2.zero;
        }

        GameObject randomPlatform = activePlatforms[Random.Range(0, activePlatforms.Count)];

        GameObject childPlatform = randomPlatform.transform.GetChild(0).gameObject;

        if (childPlatform != null)
        {
            return childPlatform.transform.position;
        }
        else
        {
            Debug.LogWarning("Child object 'Plattform 1' not found in " + randomPlatform.name);
            return Vector2.zero;
        }
    }

    private void printList()
    {
        string log = "";
        foreach (GameObject platform in activePlatforms)
        {
            log += platform.name + ", ";
        }
        Debug.Log("Active Platforms: " + log.TrimEnd(',', ' '));
    }
}
