using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIView : MonoBehaviour
{
    private LevelUIController levelUIController;
    [SerializeField] Button GoBackButton;
    [SerializeField] List<LevelButtonCollection> levelButtonCollection = new List<LevelButtonCollection>();

    private void Start()
    {
        GoBackButton.onClick.AddListener(OnGoBackButtonClicked);
    }

    public void SetController(LevelUIController levelUIController)
    {
        this.levelUIController = levelUIController;
    }

    public void OpenLevel(int levelID)
    {
        levelUIController.OnLevelButtonClicked(levelID);
    }

    public void OnGoBackButtonClicked()
    {
        levelUIController.OnGoBackButtonClicked();
    }

    public List<LevelButtonCollection> GetLevelButtonCollection()=> levelButtonCollection;

}

public enum LevelStatus
{
    OPENED,
    CLOSED,
    COMPLETED
}

[System.Serializable]
public class LevelButtonCollection
{
    [HideInInspector] public LevelStatus status;
    public Button levelButton;
    public int levelCount;
}