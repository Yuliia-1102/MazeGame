using System.Collections;
using UnityEngine;
using UnityEngine.Device;

public class Key : MonoBehaviour
{
    public ResourceSpawner spawnedKeys;
    public WinScreen winScreen;
    public PlayerResource playerResource;

    public PlayerManager playerController;

    private void Update()
    {
        if (spawnedKeys != null && playerResource != null && winScreen != null)
        {
            if (spawnedKeys.artifactCount == playerResource.NumberOfKeys)
            {
                StartCoroutine(ShowGameOverScreenWithDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerResource playerResource = other.GetComponent<PlayerResource>();

        if (playerResource != null )
        {
            playerResource.KeysCollected();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowGameOverScreenWithDelay()
    {
        yield return new WaitForSeconds(0.15f);
        playerController.enabled = false;
        winScreen.Setup();
    }
}
