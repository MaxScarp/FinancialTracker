using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Database
{
    public const string OUTCOME_COLLECTION_NAME = "Outcomes";

    public struct DatabaseResult
    {
        public Dictionary<string, OutcomeDatabase> OutcomeDatabaseDictionary;
        public string ErrorMessage;
    }

    public static FirebaseFirestore Db { get; private set; }

    public static void Init()
    {
        Db = FirebaseFirestore.DefaultInstance;
        if (Db == null)
        {
            Debug.LogError("Error: Could not find a valid instance of Firestore database!");
            return;
        }
    }

    public static async Task<string> AddOutcome(OutcomeDatabase outcomeDatabase)
    {
        string errorString = string.Empty;

        DocumentReference documentReference = Db.Collection(OUTCOME_COLLECTION_NAME).Document(DateTime.Now.ToString("F"));
        await documentReference.SetAsync(outcomeDatabase).ContinueWith(task =>
        {
            if (!task.IsCompletedSuccessfully)
            {
                errorString = task.Exception.Message;
            }
        });

        return errorString;
    }

    public static async Task<DatabaseResult> ReadAllOutcomeCollection()
    {
        Dictionary<string, OutcomeDatabase> outcomeDatabaseList = new Dictionary<string, OutcomeDatabase>();

        Query outcomeQuery = Db.Collection(OUTCOME_COLLECTION_NAME);
        try
        {
            QuerySnapshot outcomeQuerySnapshot = await outcomeQuery.GetSnapshotAsync(Source.Server);
            if (outcomeQuerySnapshot == null || outcomeQuerySnapshot.Count <= 0)
            {
                return new DatabaseResult { OutcomeDatabaseDictionary = null, ErrorMessage = $"No document inside {OUTCOME_COLLECTION_NAME} collection found!" };
            }

            foreach (DocumentSnapshot documentSnapshot in outcomeQuerySnapshot.Documents)
            {
                OutcomeDatabase outcomeDatabase = documentSnapshot.ConvertTo<OutcomeDatabase>();

                if (!outcomeDatabaseList.TryAdd(documentSnapshot.Id, outcomeDatabase))
                {
                    return new DatabaseResult { OutcomeDatabaseDictionary = null, ErrorMessage = $"Duplicated document ID inside {OUTCOME_COLLECTION_NAME} collection found!" };
                }
            }

            return new DatabaseResult { OutcomeDatabaseDictionary = outcomeDatabaseList, ErrorMessage = string.Empty };
        }
        catch (Exception databaseError)
        {
            return new DatabaseResult { OutcomeDatabaseDictionary = null, ErrorMessage = databaseError.Message };
        }
    }

    public static async Task<string> DeleteOutcome(string documentId)
    {
        string errorString = string.Empty;

        DocumentReference documentReference = Db.Collection(OUTCOME_COLLECTION_NAME).Document(documentId);
        await documentReference.DeleteAsync().ContinueWith(task =>
        {
            if (!task.IsCompletedSuccessfully)
            {
                errorString = task.Exception.Message;
            }
        });

        return errorString;
    }
}
