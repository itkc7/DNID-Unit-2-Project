using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Starte die Coroutine, um das Item erscheinen zu lassen
        StartCoroutine(ShowItemAfterDelay(2f)); // 5 Sekunden Verzögerung
    }

    // Coroutine, die das Item nach einer bestimmten Zeit erscheinen lässt
    private IEnumerator ShowItemAfterDelay(float delay)
    {
        // Das Item zunächst deaktivieren
        gameObject.SetActive(false); // Deaktiviere das Item

        // Warten, bis die Verzögerung abgelaufen ist
        yield return new WaitForSeconds(delay);

        // Das Item aktivieren
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Hier kannst du andere Logik für das Item hinzufügen, wenn nötig
    }
}
