using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectToShowArray;
    [SerializeField] private GameObject[] gameObjectToHideArray;

    private void Awake()
    {
        SetActiveAllGameObjects();
    }

    private void Start()
    {
        HideOrShowGameObjects();
    }

    private void SetActiveAllGameObjects()
    {
        GameObject[] allGameObjectArray = new GameObject[gameObjectToShowArray.Length + gameObjectToHideArray.Length];

        for (int i = 0; i < gameObjectToShowArray.Length; i++)
        {
            allGameObjectArray[i] = gameObjectToShowArray[i];
        }
        for (int i = 0; i < gameObjectToHideArray.Length; i++)
        {
            allGameObjectArray[i + gameObjectToShowArray.Length] = gameObjectToHideArray[i];
        }

        foreach (GameObject gameObject in allGameObjectArray)
        {
            gameObject.SetActive(true);
        }
    }

    private void HideOrShowGameObjects()
    {
        foreach (GameObject gameObject in gameObjectToShowArray)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in gameObjectToHideArray)
        {
            gameObject.SetActive(false);
        }
    }
}
