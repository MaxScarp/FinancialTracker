using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCategoryList", menuName = "ScriptableObjects/CategoryList")]
public class CategoryListSO : ScriptableObject
{
    public List<CategorySO> List;
}
