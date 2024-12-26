using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView:MonoBehaviour
{
    private PlayerUIController playerUIController;
    [SerializeField] Slider playerHealthBar;

    public void SetController(PlayerUIController playerUIController)
    {
        this.playerUIController = playerUIController;
    }

    public Slider GetPlayerHealthBar() => playerHealthBar;

}