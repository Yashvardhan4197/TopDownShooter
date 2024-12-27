
using UnityEngine;

public class GameWinManager : MonoBehaviour
{
    [SerializeField] Animator chestAnimator;
    [SerializeField] GameObject gameWinDoor;

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
        gameWinDoor?.SetActive(false);
    }

}
