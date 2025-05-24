using TMPro;
using UnityEngine;

public class AboutCreator : MonoBehaviour
{
    [Header("About Section")]
    [SerializeField] private TextMeshProUGUI _aboutSection;
    [SerializeField] private string _aboutText;
    [SerializeField] private string _gameName = "�������";
    [SerializeField] private string _groupName = "̲-31";
    [SerializeField] private string _authorName = "ѳ��� ��� ������������";

    private void Awake()
    {
        CheckFields();
        FillTextComponent();
    }

    private void OnValidate()
    {
        CheckFields();
        FillTextComponent();
    }

    private void CheckFields()
    {
        if (_aboutSection == null)
        {
            Debug.LogError($"Text component at {this.gameObject.name} was not assigned!");
        }

        if (string.IsNullOrEmpty(_aboutText))
        {
            if (string.IsNullOrEmpty(_authorName) == false && string.IsNullOrEmpty(_groupName) == false && string.IsNullOrEmpty(_gameName) == false)
            {
                _aboutText = $"��� �� ������ {_gameName}\n" +
                             $"�������� ��������� ����� {_groupName}\n" +
                             $"������� ��'� �� �������";
            }
            else
            {
                Debug.LogError($"About text was not assigned and some of the fields are empty!");
                _aboutText = $"��� �� ������ Rickroll\n" +
                             $"�������� ��������� ����� �����\n" +
                             $"������� ��'� �� �������";
            }
        }
    }

    private void FillTextComponent()
    {
        _aboutSection.text = _aboutText;    
    }
}
