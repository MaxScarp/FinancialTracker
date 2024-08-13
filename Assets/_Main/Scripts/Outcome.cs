using System;
using UnityEngine;

public class Outcome
{
    public event EventHandler<CategorySO> OnCategoryChanged;

    private float money;
    private CategorySO category;
    private int subCategoryIndex;

    public void SetMoney(float money)
    {
        this.money = money;

        //DEBUG
        Debug.Log($"Money: {this.money}");
    }

    public void SetCategory(CategorySO category)
    {
        this.category = category;

        //DEBUG
        Debug.Log($"Category: {this.category.Name}");

        OnCategoryChanged?.Invoke(this, this.category);
    }

    public void SetSubCateogryIndex(int subCategoryIndex)
    {
        this.subCategoryIndex = subCategoryIndex;

        //DEBUG
        Debug.Log($"SubCategoryName: {category.subCategory.SubcategoryArray[this.subCategoryIndex]}");
    }
}
