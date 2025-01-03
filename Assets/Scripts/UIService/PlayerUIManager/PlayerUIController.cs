﻿
using System;

public class PlayerUIController
{
    private PlayerUIView playerUIView;
    public PlayerUIController(PlayerUIView playerUIView)
    {
        this.playerUIView = playerUIView;
        GameService.Instance.StartGameAction += OnGameStart;
    }

    private void OpenPlayerUI()
    {
        playerUIView.gameObject.SetActive(true);
    }

    public void OnGameStart()
    {
        OpenPlayerUI();
    }

    public void ClosePlayerUI()
    {
        playerUIView.gameObject.SetActive(false);
    }


    public void UpdateHealthBar(float health)
    {
        playerUIView.GetPlayerHealthBar().value = health;
    }

    public void SetMaxHealth(float maxhealth)
    {
        playerUIView.GetPlayerHealthBar().maxValue = maxhealth;
    }

}

