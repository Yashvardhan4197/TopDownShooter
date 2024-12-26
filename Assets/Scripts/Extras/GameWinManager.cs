using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinManager : MonoBehaviour
{
    [SerializeField] Animator chestAnimator;


    private void Start()
    {
        chestAnimator.SetBool("isWin", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            chestAnimator.SetBool("isWin", true);
        }
    }
    
    public void OnGameWon()
    {
        GameService.Instance.UIService.GetInGameUIController().OpenGameWinScreen();
        GameService.Instance.UIService.GetLevelUIController().SetLevelStatusCompleted(GameService.Instance.LevelService.CurrentLevel);
    }

}
