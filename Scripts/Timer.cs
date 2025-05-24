using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int startMinutes;
    private int startSeconds;
    public float remainingSeconds;
    private int count = 0;

    public bool timeIsRunning = false;
    public TextMeshProUGUI timerText;

    private int timeAdded;
    public TextMeshProUGUI timePlus;

    public GameOverScreen screen;
    public PlayerManager playerController;
    public PlayerHealth playerHealth;

    public ResourceSpawner spawnedKeys;
    public PlayerResource playerResource;
    void Update()
    {
        if (timeIsRunning)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds -= Time.deltaTime;

                if (remainingSeconds <= 0)
                {
                    remainingSeconds = 0;
                    timeIsRunning = false;
                    StartCoroutine(ShowGameOverScreenWithDelay());
                }

                displayTime();
            }
        }

        if (playerHealth != null && playerHealth.health == 0)
        {
            timeIsRunning = false;
        }

        if (spawnedKeys != null && playerResource != null && spawnedKeys.artifactCount == playerResource.NumberOfKeys)
        {
            timeIsRunning = false;
        }
    }

    void displayTime()
    {
        if (timeIsRunning)
        {
            int minutes = Mathf.FloorToInt(remainingSeconds / 60);
            int seconds = Mathf.FloorToInt(remainingSeconds % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Timer"))
        {
            startMinutes = Random.Range(0, 1); 
            startSeconds = Random.Range(20, 60); 

            if (count == 0)
            {
                remainingSeconds = (startMinutes * 60 + startSeconds) + 1;
                timeIsRunning = true;
                other.gameObject.SetActive(false);
                count++;
            }
            else
            {
                timeAdded = Random.Range(5, 20);
                int sign = Random.Range(0, 2) == 0 ? -1 : 1;

                int delta = timeAdded * sign;
                remainingSeconds += delta;
                remainingSeconds = Mathf.Max(0, remainingSeconds);

                timePlus.text = (sign > 0) ? $"+ {timeAdded} сек" : $"- {timeAdded} сек";

                Destroy(other.gameObject);

                if (remainingSeconds == 0 && timeIsRunning)
                {
                    timeIsRunning = false; 

                    StartCoroutine(ShowGameOverScreenWithDelay());
                }
            }
        }
    }

    private IEnumerator ShowGameOverScreenWithDelay()
    {
        yield return new WaitForSeconds(0.15f);
        playerController.enabled = false;
        string reasonOfLoss = "Час закінчився";
        screen.Setup(reasonOfLoss);
    }
}
