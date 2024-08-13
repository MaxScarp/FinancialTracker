using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSubCategoryList", menuName = "ScriptableObjects/SubCategoryList")]
public class SubCategoryListSO : ScriptableObject
{
    public List<SubCategorySO> List;
}