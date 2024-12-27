using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private PlayerDataSO playerData;
    private float currentHealth;
    private bool isShieldActive;
    private float shieldTimeDuration;
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
        GameService.Instance.UIService.GetPlayerUIController().SetMaxHealth(playerData.Health);
        GameService.Instance.UIService.GetPlayerUIController().UpdateHealthBar(currentHealth);
        playerView.transform.position = playerData.StartPositionsForEachLevel[GameService.Instance.LevelService.CurrentLevel - 1].position;
        DisableShield();
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
        if (isShieldActive == false)
        {
            currentHealth -= Mathf.Abs(damage);
            GameService.Instance.UIService.GetPlayerUIController().UpdateHealthBar(currentHealth);
            GameService.Instance.SoundService.PlaySFX(Sound.PLAYER_DAMAGE);
            if (currentHealth <= 0)
            {
                GameService.Instance.UIService.GetInGameUIController().OpenGameLostScreen();
            }
        }
    }

    public void AddPlayerHealth(float health)
    {
        if (currentHealth + health >= playerData.Health)
        {
            currentHealth = playerData.Health;
        }
        else
        {
            currentHealth += health;
        }
        GameService.Instance.UIService.GetPlayerUIController().UpdateHealthBar(currentHealth);
    }


    public void TogglePause()
    {
        GameService.Instance.UIService.GetInGameUIController().TogglePause();
    }

    public void SetShieldStatus(bool shield)
    {
        isShieldActive = shield;

        if(isShieldActive==false)
        {
            playerView.GetSpriteRenderer().color = Color.white;
        }
        else
        {
            playerView.GetSpriteRenderer().color = Color.cyan;
        }

    }


    public void Update()
    {
        if (isShieldActive == true && Time.time >= shieldTimeDuration)
        {
            DisableShield();
        }
    }

    private void DisableShield()
    {
        isShieldActive = false;
        SetShieldStatus(isShieldActive);
    }

    public void ActivateShield(float shieldTime)
    {
        shieldTimeDuration = Time.time + shieldTime;
        isShieldActive = true;
        SetShieldStatus(isShieldActive);
    }

    public void UpdateWeaponSpriteOrder(Vector2 playerPos)
    {
        if(playerPos.y<0)
        {
            GameService.Instance.WeaponService.GetWeaponController().SetWeaponSpriteOrder(1);
        }
        else
        {
            GameService.Instance.WeaponService.GetWeaponController().SetWeaponSpriteOrder(0);
        }
    }

}