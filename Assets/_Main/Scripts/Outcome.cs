using Firebase.Firestore;
using System;

public class Outcome
{
    public event EventHandler<CategorySO> OnCategoryChanged;

    private float money;
    private CategorySO category;
    private int subCategoryIndex;

    public Outcome()
    {
        money = -1.0f;
        category = null;
        subCategoryIndex = -1;
    }

    public void SetMoney(float money)
    {
        this.money = money;
    }

    public void SetCategory(CategorySO category)
    {
        this.category = category;

        OnCategoryChanged?.Invoke(this, this.category);
    }

    public void SetSubCateogryIndex(int subCategoryIndex)
    {
        this.subCategoryIndex = subCategoryIndex;
    }

    public OutcomeDatabase GenerateDataForDatabase()
    {
        OutcomeDatabase data = new OutcomeDatabase
        {
            ServerTimestamp = FieldValue.ServerTimestamp,
            Email = Authenticator.User.Email,
            MacroCategory = category.CategoryType,
            Category = category.Name,
            SubCategory = category.subCategory.SubcategoryArray[subCategoryIndex],
            Money = money
        };

        return data;
    }

    public bool IsReadyToGenerateData()
    {
        return money > 0.0f && category != null && subCategoryIndex >= 0;
    }
}
