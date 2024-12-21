using System;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private PlayerDataSO playerData;


    public PlayerController(PlayerView playerView,PlayerDataSO playerData)
    {
        this.playerView = playerView;
        this.playerView.SetController(this);
        this.playerData = playerData;
    }

    public void OnGameStart()
    {
        playerView.GetAnimator().SetFloat("X", 0);
        playerView.GetAnimator().SetFloat ("Y", 0);
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

}