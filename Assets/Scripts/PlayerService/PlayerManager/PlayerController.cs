using System;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private PlayerDataSO playerData;
    private float currentHealth;

    public PlayerController(PlayerView playerView,PlayerDataSO playerData)
    {
        this.playerView = playerView;
        this.playerView.SetController(this);
        this.playerData = playerData;
        GameService.Instance.StartGameAction += OnGameStart;
    }

    public void OnGameStart()
    {
        playerView.GetAnimator().SetFloat("X", 0);
        playerView.GetAnimator().SetFloat ("Y", 0);
        currentHealth = playerData.Health;
    }


    public void MovePlayer(Vector2 movement)
    {
        playerView.GetRigidbody2D().MovePosition(playerView.GetRigidbody2D().position + movement* playerData.MovementSpeed * Time.fixedDeltaTime);
    }

    public void SetPlayerDirection(float x, float y)
    {
        playerView.GetAnimator().SetFloat("X", x);
        playerView.GetAnimator().SetFloat("Y", y);
    }

    public Transform GetPlayerTransform()
    {
        return playerView?.transform;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Restart Section Player DEAD
            Debug.Log("Player Dead");
        }
    }

    public void TogglePause()
    {
        GameService.Instance.UIService.GetInGameUIController().TogglePause();
    }
}