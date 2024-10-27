using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public PlatformManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject parentObject = transform.parent.gameObject;
            manager.PlatformTriggered(parentObject);
        }
    }
}