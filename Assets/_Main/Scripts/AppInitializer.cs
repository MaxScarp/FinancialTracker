using Firebase;
using Firebase.Crashlytics;
using Firebase.Extensions;
using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectToShowArray;
    [SerializeField] private GameObject[] gameObjectToHideArray;

    [SerializeField] private GameObject[] gameObjectToStayOnTopArray;

    [SerializeField] private GameObject authMenuGameObject;

    public FirebaseApp App { get; private set; }

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError($"Error: Could not resolve all Firebase dependencies {dependencyStatus}!");

                Crashlytics.ReportUncaughtExceptionsAsFatal = true;

                return;
            }

            App = FirebaseApp.DefaultInstance;
        });

        Authenticator.Init();
        Database.Init();

        SetActiveAllGameObjects();
    }

    private void Start()
    {
        HideOrShowGameObjects();

        foreach (GameObject gameObject in gameObjectToStayOnTopArray)
        {
            gameObject.transform.SetAsLastSibling();
        }

        //TODO - Ricontrolla come gestire login e logout dalla documentazione firebase,
        //       invia degli eventi ogni volta che l'utente si connette o disconnette e modifica una variabile booleana,
        //       questa variabile salvala nel PlayerPref e poi usala ogni volta che apri la applicazione,
        //       se l'utente ha eseguito un logout lo sai grazie a questa variabile, lo sai anche per il login eventuale.
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
