using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutcomePresenter : MonoBehaviour
{
    public event EventHandler OnBackButtonSelected;

    [SerializeField] private GameObject addOutcomeGameObject;

    [SerializeField] private MessagePresenter messagePresenter;

    [SerializeField] private CategoryListSO categoryList;

    [SerializeField] private TMP_InputField moneyInputFiedl;
    [SerializeField] private TMP_Dropdown categoryDropdown;
    [SerializeField] private TMP_Dropdown subCategoryDropdown;
    [SerializeField] private Button addButton;
    [SerializeField] private Button backButton;

    private CategorySO selectedCategory;
    private Outcome outcome;

    private void Awake()
    {
        List<string> categoryNameList = new List<string>();
        foreach (CategorySO category in categoryList.List)
        {
            categoryNameList.Add(category.Name);
        }

        categoryDropdown.AddOptions(categoryNameList);
    }

    private void Start()
    {
        addButton.onClick.AddListener(() =>
        {
            messagePresenter.Show("Outcome added succesfully", Color.green);
        });
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonSelected?.Invoke(this, EventArgs.Empty);
        });
    }

    public void Show()
    {
        addOutcomeGameObject.SetActive(true);

        outcome = new Outcome();

        InitializeUI();
        AddListeners();
    }

    public void Hide()
    {
        InitializeUI();
        RemoveListeners();

        addOutcomeGameObject.SetActive(false);
    }

    private void RemoveListeners()
    {
        moneyInputFiedl.onValueChanged.RemoveAllListeners();
        categoryDropdown.onValueChanged.RemoveAllListeners();

        outcome.OnCategoryChanged -= OutcomeModel_OnCategoryChanged;
    }

    private void AddListeners()
    {
        moneyInputFiedl.onValueChanged.AddListener((string newText) =>
        {
            if (!float.TryParse(newText, out float money))
            {
                return;
            }

            outcome.SetMoney(money);
        });

        categoryDropdown.onValueChanged.AddListener((int index) =>
        {
            string selectedCategoryName = categoryDropdown.options[index].text;
            selectedCategory = categoryList.List.FirstOrDefault(c => c.Name == selectedCategoryName);
            if (!selectedCategory)
            {
                Debug.LogError($"Error: Trying to find a {nameof(CategorySO)} into {nameof(categoryList)}!");
                return;
            }

            outcome.SetCategory(selectedCategory);

            subCategoryDropdown.enabled = true;
        });

        outcome.OnCategoryChanged += OutcomeModel_OnCategoryChanged;
    }

    private void InitializeUI()
    {
        moneyInputFiedl.SetTextWithoutNotify(string.Empty);
        categoryDropdown.SetValueWithoutNotify(-1);
        subCategoryDropdown.SetValueWithoutNotify(-1);

        subCategoryDropdown.enabled = false;
    }

    private void OutcomeModel_OnCategoryChanged(object sender, CategorySO category)
    {
        UpdateSubCategoryDropdown(category);
    }

    private void UpdateSubCategoryDropdown(CategorySO category)
    {
        subCategoryDropdown.onValueChanged.RemoveAllListeners();
        subCategoryDropdown.ClearOptions();

        List<string> subCategoryNameList = new List<string>();
        foreach (string name in category.subCategory.SubcategoryArray)
        {
            subCategoryNameList.Add(name);
        }

        subCategoryDropdown.AddOptions(subCategoryNameList);
        subCategoryDropdown.value = -1;

        subCategoryDropdown.onValueChanged.AddListener((int index) =>
        {
            outcome.SetSubCateogryIndex(index);
        });
    }
}
