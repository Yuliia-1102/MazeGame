using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    private TextMeshProUGUI keyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateKeyText(PlayerResource playerResource)
    {
        keyText.text = playerResource.NumberOfKeys.ToString();
    }
}
