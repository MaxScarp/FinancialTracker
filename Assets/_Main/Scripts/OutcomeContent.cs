using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutcomeContent : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private TextMeshProUGUI subCategoryText;

    private string documentId;

    public void Init(OutcomeDatabase outcomeDatabase, string documentId)
    {
        moneyText.text = $"{outcomeDatabase.Money:.00}€";
        categoryText.text = outcomeDatabase.Category;
        subCategoryText.text = outcomeDatabase.SubCategory;

        this.documentId = documentId;

        toggle.isOn = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool IsOn() => toggle.isOn;
    public string GetDocumentId() => documentId;
}
