using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectToShowArray;
    [SerializeField] private GameObject[] gameObjectToHideArray;

    [SerializeField] private GameObject[] gameObjectToStayOnTopArray;

    public FirebaseApp App { get; private set; }
    public FirebaseFirestore Database { get; private set; }

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError($"Error: Could not resolve all Firebase dependencies {dependencyStatus}!");
                return;
            }

            App = FirebaseApp.DefaultInstance;
        });

        Database = FirebaseFirestore.DefaultInstance;
        if (Database == null)
        {
            Debug.LogError("Error: Could not find a valid instance of Firestore database!");
            return;
        }

        Authenticator.Init();

        SetActiveAllGameObjects();
    }

    private void Start()
    {
        HideOrShowGameObjects();

        foreach (GameObject gameObject in gameObjectToStayOnTopArray)
        {
            gameObject.transform.SetAsLastSibling();
        }
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
