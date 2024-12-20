using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController
{
    private WeaponView weaponView;
    private WeaponDataSO weaponDataSO;
    private Dictionary<int,WeaponBody> spawnedWeapons=new Dictionary<int, WeaponBody>();
    private int currentWeaponIndex;
    private float nextTimetoFire;
    public WeaponController(WeaponView weaponView,WeaponDataSO weaponDataSO)
    {
        this.weaponView = weaponView;
        this.weaponView.SetController(this);
        this.weaponDataSO = weaponDataSO;
        currentWeaponIndex = -1;
        Initialize();
        //Change this later
        OnGameStart();
    }

    private void Initialize()
    {
        for(int i = 0;i<weaponDataSO.Weapons.Count;i++)
        {
            WeaponBody weaponBody = UnityEngine.Object.Instantiate(weaponDataSO.Weapons[i].WeaponBody);
            weaponBody.transform.SetParent(weaponView.transform,false);
            weaponBody.gameObject.SetActive(false);
            spawnedWeapons.Add(i, weaponBody);
        }
    }

    public void OnGameStart()
    {
        currentWeaponIndex = -1;
    }

    public void OnWeaponChooseButtonClicked(int index)
    {
        if (weaponDataSO.Weapons.Count > 0 && index<=weaponDataSO.Weapons.Count)
        {
            foreach(var item in spawnedWeapons)
            {
                if(item.Key != index-1)
                {
                    item.Value.gameObject.SetActive(false);
                }
            }
            if (currentWeaponIndex==index-1)
            {
                
                spawnedWeapons[index - 1].gameObject.SetActive(false);
                currentWeaponIndex = -1;
            }
            else
            {
                spawnedWeapons[index - 1].gameObject.SetActive(true);
                currentWeaponIndex = index - 1;
            }
            nextTimetoFire = 0;
        }
    }

    public void Shoot()
    {
        if (currentWeaponIndex != -1)
        {
            if (Time.time >= nextTimetoFire)
            {
                nextTimetoFire = Time.time + (1 / weaponDataSO.Weapons[currentWeaponIndex].FireRate);

                Ray2D ray2D = new Ray2D();
                RaycastHit2D hit2D;
                ray2D.origin = spawnedWeapons[currentWeaponIndex].GetMuzzleTransform().position;
                ray2D.direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnedWeapons[currentWeaponIndex].GetMuzzleTransform().position).normalized;

                hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, weaponDataSO.Weapons[currentWeaponIndex].MaxRange);
                var tracer = UnityEngine.Object.Instantiate(weaponDataSO.Weapons[currentWeaponIndex].BulletTrail, ray2D.origin, Quaternion.identity);
                tracer.AddPosition(ray2D.origin);
                tracer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //check if DamageAble object

            }
        }

    }

    public void FlipCurrentWeapon(float angle)
    {
        if (currentWeaponIndex != -1)
        {
            float x= MathF.Abs(spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale.x);
            float y = MathF.Abs(spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale.y);
            float z = MathF.Abs(spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale.z);
            spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale = new Vector3(x, y, z);
            Vector3 weaponScale= spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale;
            if(angle > 90||angle<-90)
            {
                weaponScale.y = -1f;
            }
            else
            {
                weaponScale.y = 1f;
            }
            spawnedWeapons[currentWeaponIndex].gameObject.transform.localScale=weaponScale;
        }
    }

    public void ToggleShootAnimation(bool status)
    {
        if (currentWeaponIndex != -1)
        {
            spawnedWeapons[currentWeaponIndex].GetWeaponAnimController().SetBool("shoot", status);
        }
    }


}