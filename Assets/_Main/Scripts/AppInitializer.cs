using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectToShowArray;
    [SerializeField] private GameObject[] gameObjectToHideArray;

    public FirebaseApp App { get; private set; }

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                App = FirebaseApp.DefaultInstance;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}!");
                return;
            }
        });

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
