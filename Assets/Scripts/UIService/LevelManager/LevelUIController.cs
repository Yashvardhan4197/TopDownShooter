using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController
{
    private LevelUIView levelUIView;

    public LevelUIController(LevelUIView levelUIView)
    {
        this.levelUIView = levelUIView;
        this.levelUIView.SetController(this);
    }

    public void OpenLevelUISection()
    {
        levelUIView.gameObject.SetActive(true);
        RefreshLevelButtonStatus();
    }

    public void CloseLevelUISection()
    {
        levelUIView.gameObject.SetActive(false);
    }

    //Call this to refresh all level status
    public void RefreshLevelButtonStatus()
    {
        List<LevelButtonCollection> levelButtonCollection = levelUIView.GetLevelButtonCollection();
        if (PlayerPrefs.GetInt("level" + 1, -1) == -1)
        {
            ResetLevelButtonData();
            return;
        }
        SetLevelButtonStatus(levelButtonCollection);

    }

    private void SetLevelButtonStatus(List<LevelButtonCollection> levelButtonCollection)
    {
        for (int i = 0; i < levelButtonCollection.Count; i++)
        {
            int temp = PlayerPrefs.GetInt("level" + (i + 1), 0);
            if (temp == 0)
            {
                levelButtonCollection[i].levelButton.interactable = true;
                levelButtonCollection[i].levelButton.image.color = Color.yellow;
                levelButtonCollection[i].status = LevelStatus.OPENED;
            }
            else if (temp == 1)
            {
                levelButtonCollection[i].levelButton.interactable = false;
                levelButtonCollection[i].levelButton.image.color = Color.red;
                levelButtonCollection[i].status = LevelStatus.CLOSED;
            }
            else if (temp == 2)
            {
                levelButtonCollection[i].levelButton.interactable = true;
                levelButtonCollection[i].levelButton.image.color = Color.green;
                levelButtonCollection[i].status = LevelStatus.COMPLETED;
            }
        }
    }

    private void ResetLevelButtonData()
    {
        List<LevelButtonCollection> levelButtonCollection = levelUIView.GetLevelButtonCollection();
        for (int i=0;i<levelButtonCollection.Count;i++)
        {
            if (i == 0)
            {
                PlayerPrefs.SetInt("level" + (i + 1), 0);
            }
            else
            {
                PlayerPrefs.SetInt("level" + (i + 1), 1);
            }
        }
        SetLevelButtonStatus(levelButtonCollection);
    }


    //call this to set the level as completed
    public void SetLevelStatusCompleted(int level)
    {
        int levelNumber = 0;
        List<LevelButtonCollection> levelButtonCollection = levelUIView.GetLevelButtonCollection();
        for (int i = 0; i < levelUIView.GetLevelButtonCollection().Count; i++)
        {
            if (level == levelButtonCollection[i].levelCount)
            {
                levelNumber = i;
                break;
            }
        }
        levelUIView.GetLevelButtonCollection()[levelNumber].status = LevelStatus.COMPLETED;
        levelButtonCollection[levelNumber].levelButton.image.color = Color.green;
        levelButtonCollection[levelNumber].status = LevelStatus.COMPLETED;
        PlayerPrefs.SetInt("level" + level, 2);
        if(levelNumber+1<levelButtonCollection.Count)
        {
            if(levelButtonCollection[levelNumber+1].status!=LevelStatus.COMPLETED)
            {
                levelButtonCollection[levelNumber+1].levelButton.interactable = true;
                levelButtonCollection[levelNumber+1].levelButton.image.color = Color.yellow;
                levelButtonCollection[levelNumber+1].status = LevelStatus.OPENED;
                PlayerPrefs.SetInt("level" + (level+1), 0);
            }
        }
    }

    public void OnLevelButtonClicked(int levelID)
    {
        GameService.Instance.LevelService.LoadLevel(levelID);
    }

    public void OnGoBackButtonClicked()
    {
        levelUIView.gameObject.SetActive(false);
    }
}