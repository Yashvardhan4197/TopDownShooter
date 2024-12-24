
using System;
using UnityEngine;


public class WeaponView : MonoBehaviour
{

    private WeaponController weaponController;
    public void SetController(WeaponController weaponController)
    {
        this.weaponController = weaponController;
    }

    private void Update()
    {
        Shoot();
        WeaponMove();
        ChooseWeapon();
        ReloadWeapon();
    }

    private void ReloadWeapon()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponController?.ReloadCurrentWeapon();
        }
    }

    private void ChooseWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponController.OnWeaponChooseButtonClicked(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponController.OnWeaponChooseButtonClicked(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponController.OnWeaponChooseButtonClicked(3);
        }
    }

    private void Shoot()
    {
        if(Input.GetMouseButton(0))
        {
            weaponController.Shoot();
            
        }
        else
        {
            weaponController.ToggleShootAnimation(false);
        }
    }

    private void WeaponMove()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        weaponController.FlipCurrentWeapon(angle);
        //changes player direction
        //weaponController.SetPlayerDirection(angle);
    }



}
