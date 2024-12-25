
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2 movement;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Animator playerAnimator;
    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
        movement=Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = movement.normalized;
        SetAnimation(movement);
        TogglePause();
    }

    private void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.TogglePause();
        }
    }

    private void SetAnimation(Vector2 movement)
    {
        bool ismoving = false;
        if(Mathf.Abs(movement.magnitude)>0.1f)
        {
            ismoving = true;
        }
        if(ismoving)
        {
            playerAnimator.SetFloat("X", movement.x);
            playerAnimator.SetFloat("Y", movement.y);
            playerAnimator.SetBool("running", true);
        }
        else
        {
            playerAnimator.SetBool("running", false);
        }
    }

    private void Move()
    { 
        playerController.MovePlayer(movement);
    }

    public Rigidbody2D GetRigidbody2D() => rb2D;
    public Animator GetAnimator() => playerAnimator;
}
