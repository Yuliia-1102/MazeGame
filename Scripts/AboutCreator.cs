using TMPro;
using UnityEngine;

public class AboutCreator : MonoBehaviour
{
    [Header("About Section")]
    [SerializeField] private TextMeshProUGUI _aboutSection;
    [SerializeField] private string _aboutText;
    [SerializeField] private string _gameName = "Лабіринт";
    [SerializeField] private string _groupName = "МІ-31";
    [SerializeField] private string _authorName = "Сірак Юлія Олександрівна";

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
                _aboutText = $"Гра під назвою {_gameName}\n" +
                             $"Виконана студентом групи {_groupName}\n" +
                             $"Прізвище Ім'я По батькові";
            }
            else
            {
                Debug.LogError($"About text was not assigned and some of the fields are empty!");
                _aboutText = $"Гра під назвою Rickroll\n" +
                             $"Виконана студентом групи ГРУПА\n" +
                             $"Прізвище Ім'я По батькові";
            }
        }
    }

    private void FillTextComponent()
    {
        _aboutSection.text = _aboutText;    
    }
}
