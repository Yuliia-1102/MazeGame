using UnityEngine;
using TMPro;

public class AnotherUI : MonoBehaviour
{
    private TextMeshProUGUI maxValueText;

    void Start()
    {
        maxValueText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateMaxValueText(int spawnersCount)
    {
        maxValueText.text = spawnersCount.ToString();
    }
}
