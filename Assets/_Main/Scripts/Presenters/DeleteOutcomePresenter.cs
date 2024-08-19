using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeleteOutcomePresenter : MonoBehaviour
{
    public event EventHandler OnBackButtonSelected;

    [SerializeField] private GameObject deleteOutcomeGameObject;
    [SerializeField] private MessageYesNoPresenter messageYesNoPresenter;
    [SerializeField] private Transform outcomeContentContainerTransform;
    [SerializeField] private Transform outcomeContentPrefab;

    [SerializeField] private MessagePresenter messagePresenter;

    [SerializeField] private Button deleteButton;
    [SerializeField] private Button backButton;

    private Database.DatabaseResult databaseResult;

    private void Start()
    {
        deleteButton.onClick.AddListener(() =>
        {
            messageYesNoPresenter.Show("Are you sure you want to delete the selected outcomes from the database?");
        });
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonSelected?.Invoke(this, EventArgs.Empty);
        });
    }

    private async void MessageYesNoPresenter_OnYesButtonSelected(object sender, EventArgs e)
    {
        bool isDeleteTaskCompleted = await DeleteSelectedOutcomes();
        if (!isDeleteTaskCompleted)
        {
            return;
        }

        messagePresenter.Show("Selected outcomes deleted succesfully", Color.green);
    }

    private async Task<bool> DeleteSelectedOutcomes()
    {
        List<string> documentIdToDeleteList = new List<string>();
        List<OutcomeContent> outcomeContentToDestroyList = new List<OutcomeContent>();
        foreach (Transform child in outcomeContentContainerTransform)
        {
            if (!child.TryGetComponent(out OutcomeContent outcomeContent))
            {
                continue;
            }
            if (!outcomeContent.IsOn())
            {
                continue;
            }

            documentIdToDeleteList.Add(outcomeContent.GetDocumentId());
            outcomeContentToDestroyList.Add(outcomeContent);
        }

        foreach (string documentId in documentIdToDeleteList)
        {
            string errorMessage = await Database.DeleteOutcome(documentId);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                messagePresenter.Show(errorMessage, Color.red);
                return false;
            }
        }

        foreach (OutcomeContent outcomeContent in outcomeContentToDestroyList)
        {
            outcomeContent.Destroy();
        }

        return true;
    }

    public async void Show()
    {
        deleteOutcomeGameObject.SetActive(true);

        databaseResult = await Database.ReadAllOutcomeCollection();
        if (databaseResult.OutcomeDatabaseDictionary == null)
        {
            messagePresenter.Show(databaseResult.ErrorMessage, Color.red);

            OnBackButtonSelected?.Invoke(this, EventArgs.Empty);

            return;
        }

        foreach (KeyValuePair<string, OutcomeDatabase> keyValuePair in databaseResult.OutcomeDatabaseDictionary)
        {
            Transform outcomeContentTransform = Instantiate(outcomeContentPrefab, outcomeContentContainerTransform);
            if (outcomeContentTransform.TryGetComponent(out OutcomeContent outcomeContent))
            {
                outcomeContent.Init(keyValuePair.Value, keyValuePair.Key);
            }
        }

        messageYesNoPresenter.OnYesButtonSelected += MessageYesNoPresenter_OnYesButtonSelected;
    }

    public void Hide()
    {
        foreach (Transform child in outcomeContentContainerTransform)
        {
            if (child.TryGetComponent(out OutcomeContent outcomeContent))
            {
                outcomeContent.Destroy();
            }
        }

        messageYesNoPresenter.OnYesButtonSelected -= MessageYesNoPresenter_OnYesButtonSelected;

        deleteOutcomeGameObject.SetActive(false);
    }
}
