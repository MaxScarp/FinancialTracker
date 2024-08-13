using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreFirstTest : MonoBehaviour
{
    [SerializeField] private AppInitializer appInitializer;

    private void Start()
    {
        AddData();
        ReadData();
    }

    private void ReadData()
    {
        CollectionReference usersCollectionReference = appInitializer.Database.Collection("users");
        usersCollectionReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Debug.Log($"User: {documentSnapshot.Id}");

                Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();

                Debug.Log($"First: {documentDictionary["First"]}");
                if (documentDictionary.TryGetValue("Middle", out object value))
                {
                    Debug.Log($"Middle: {value}");
                }
                Debug.Log($"Last: {documentDictionary["Last"]}");
                Debug.Log($"Born: {documentDictionary["Born"]}");
            }

            Debug.Log("Read all data from the users collection");
        });
    }

    private void AddData()
    {
        DocumentReference documentReference = appInitializer.Database.Collection("users").Document("alovelace");
        Dictionary<string, object> firstUser = new Dictionary<string, object>
        {
            { "First", "Ada" },
            { "Last", "Lovelace" },
            { "Born", 1815 },
        };
        documentReference.SetAsync(firstUser).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the alovelace document in the users collection");
        });

        documentReference = appInitializer.Database.Collection("users").Document("aturing");
        Dictionary<string, object> secondUser = new Dictionary<string, object>
        {
            { "First", "Alan" },
            { "Middle", "Mathison" },
            { "Last", "Turing" },
            { "Born", 1912 }
        };
        documentReference.SetAsync(secondUser).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the aturing document in the users collection");
        });
    }
}
