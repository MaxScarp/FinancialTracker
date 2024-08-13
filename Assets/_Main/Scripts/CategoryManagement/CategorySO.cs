using UnityEngine;

[CreateAssetMenu(fileName = "NewCategory", menuName = "ScriptableObjects/Category")]
public class CategorySO : ScriptableObject
{
    public CategoryTypes CategoryType;
    public string Name;
    public SubCategorySO subCategory;
}
