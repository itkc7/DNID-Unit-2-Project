using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platforms;
    private List<GameObject> activePlatforms = new List<GameObject>();

    // Public variable to set the number of active platforms
    public int numberOfActivePlatforms = 3;

    private void Start()
    {
        // Initialize the active platforms list
        InitializeActivePlatforms();

        Debug.Log("Count: " + activePlatforms.Count);
    }

    private void InitializeActivePlatforms()
    {
        // Ensure we don't exceed the number of available platforms
        int platformsToActivate = Mathf.Min(numberOfActivePlatforms, platforms.Length);

        for (int i = 0; i < platformsToActivate; i++)
        {
            GameObject platform = platforms[i];
            platform.SetActive(true);
            activePlatforms.Add(platform);
        }
    }

    public void PlatformTriggered(GameObject currentPlatform)
    {
        if (platforms.Length == 0)
        {
            Debug.LogError("No platforms defined in the manager!");
            return;
        }

        // Start the coroutine for delay and shake effect
        StartCoroutine(DelayAndShake(currentPlatform));
    }

    private IEnumerator DelayAndShake(GameObject platform)
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Shake the platform for 0.5 seconds
        Vector3 originalPosition = platform.transform.position;
        float shakeDuration = 0.8f;
        float shakeMagnitude = 0.1f;

        GameObject Platfromtrigger = platform.transform.GetChild(1).gameObject;
        //bool isPlayerOnPlatform = Physics.CheckBox(Platfromtrigger.transform.position, Platfromtrigger.GetComponent<Collider>().bounds.extents, Quaternion.identity, LayerMask.GetMask("Player"));
        if (true)
        {

            while (shakeDuration > 0)
            {
                platform.transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
                shakeDuration -= Time.deltaTime;
                yield return null;
            }

            // Reset the platform back to its original position
            platform.transform.position = originalPosition;

        
            // Deactivate the platform and activate a new platform
            platform.SetActive(false);
            activePlatforms.Remove(platform);

            ActivateRandomPlatform(platform);

        }
    }

    private void ActivateRandomPlatform(GameObject currentPlatform)
    {
        GameObject newPlatform;
        do
        {
            newPlatform = platforms[Random.Range(0, platforms.Length)];
        } while (newPlatform == currentPlatform || activePlatforms.Contains(newPlatform));

        // Add the new platform to the active platforms
        activePlatforms.Add(newPlatform);
        newPlatform.SetActive(true);

        // Ensure the number of active platforms does not exceed the limit
        if (activePlatforms.Count > numberOfActivePlatforms)
        {
            // Deactivate the extra platform if needed
            GameObject extraPlatform = activePlatforms[0]; // Get the first platform in the list
            activePlatforms.RemoveAt(0); // Remove it from the active list
            extraPlatform.SetActive(false); // Deactivate it
        }
    }

    // Method to get a random position from active platforms
    public Vector2 GetRandomActivePlatformPosition()
    {
        if (activePlatforms.Count == 0)
        {
            Debug.LogWarning("No active platforms available for item spawn.");
            return Vector2.zero;
        }

        GameObject randomPlatform = activePlatforms[Random.Range(0, activePlatforms.Count)];

        // Search for the child named "Plattform 1"
        GameObject childPlatform = randomPlatform.transform.GetChild(0).gameObject;

        if (childPlatform != null)
        {
            // Return the position of the child
            return childPlatform.transform.position;
        }
        else
        {
            Debug.LogWarning("Child object 'Plattform 1' not found in " + randomPlatform.name);
            return Vector2.zero;
        }
    }
}
