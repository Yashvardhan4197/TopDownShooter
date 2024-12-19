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

    public void MovePlayer(Vector2 movement)
    {
        playerView.GetRigidbody2D().MovePosition(playerView.GetRigidbody2D().position + movement* playerData.MovementSpeed * Time.fixedDeltaTime);
    }
}