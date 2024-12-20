
using System;
using UnityEngine;
using UnityEngine.UI;

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
            //weaponController.Shoot;
        }
    }

    private void WeaponMove()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Vector3 localscale=Vector3.one;
        if(angle>90||angle<90)
        {
            localscale.y = -1f;
        }
        else
        {
            localscale.y = 1f;
        }

        transform.localScale = localscale;
    }



}
