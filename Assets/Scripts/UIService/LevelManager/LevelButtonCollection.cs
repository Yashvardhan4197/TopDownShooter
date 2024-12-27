using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelButtonCollection
{
    [HideInInspector] public LevelStatus status;
    public Button levelButton;
    public int levelCount;
}