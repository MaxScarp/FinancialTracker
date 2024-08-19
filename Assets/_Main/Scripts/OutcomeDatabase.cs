using Firebase.Firestore;

[FirestoreData]
public class OutcomeDatabase
{
    [FirestoreProperty]
    public object ServerTimestamp { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public float Money { get; set; }

    [FirestoreProperty]
    public CategoryTypes MacroCategory { get; set; }

    [FirestoreProperty]
    public string Category { get; set; }

    [FirestoreProperty]
    public string SubCategory { get; set; }
}
