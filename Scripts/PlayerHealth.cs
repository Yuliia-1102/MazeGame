using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public Slider slider;
    public TextMeshProUGUI text;
    public Gradient gradient;
    public Image fill;

    public GameOverScreen screen;
    public PlayerManager playerController;
    //public Timer timer;

    // Update is called once per frame
    void Update()
    {
        if (health == 0f)
        {
            StartCoroutine(ShowGameOverScreenWithDelay());
        }
        
        slider.value = health;
        text.text = "Здоров'я: " + health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Touched: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            health = Mathf.Max(0f, health - 20f);
            collision.gameObject.SetActive(false);
            //Debug.Log("Health is now: " + health);
        }

        if (collision.gameObject.CompareTag("Friend"))
        {
            if (health != 100f)
                collision.gameObject.SetActive(false);
            health = Mathf.Min(health + 10f, 100f);        
        }
    }

    private IEnumerator ShowGameOverScreenWithDelay()
    {
        yield return new WaitForSeconds(0.15f); 
        
        playerController.enabled = false;

        string reasonOfLoss = "Здоров'я закінчилось";
        screen.Setup(reasonOfLoss);
    }

}
