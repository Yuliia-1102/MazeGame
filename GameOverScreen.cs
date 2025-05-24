using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI reasonText;
    public void Setup (string reason)
    {
        gameObject.SetActive (true);
        reasonText.text = reason;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
