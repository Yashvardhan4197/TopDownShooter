using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponController
{
    private WeaponView weaponView;
    private WeaponDataSO weaponDataSO;
    private Dictionary<int,WeaponBody> spawnedWeapons=new Dictionary<int, WeaponBody>();
    private int currentWeaponIndex;
    private float nextTimetoFire;
    private bool isReloading;
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
        RemoveWeapons();
        currentWeaponIndex = -1;
        ResetBullets();
    }

    private void RemoveWeapons()
    {
        for(int i=0;i<spawnedWeapons.Count;i++)
        {
            spawnedWeapons[i].gameObject.SetActive(false);
        }
    }

    private void ResetBullets()
    {
        //Update in UI too later
        for(int i=0;i<weaponDataSO.Weapons.Count;i++)
        {
            weaponDataSO.Weapons[i].SetCurrentInMagBullet(weaponDataSO.Weapons[i].MaxInMagBullets);
            weaponDataSO.Weapons[i].SetCurrentUsedBullet(weaponDataSO.Weapons[i].MaxUsedBullet);
        }
    }

    public void OnWeaponChooseButtonClicked(int index)
    {
        if (isReloading == false)
        {
            if (weaponDataSO.Weapons.Count > 0 && index <= weaponDataSO.Weapons.Count)
            {
                foreach (var item in spawnedWeapons)
                {
                    if (item.Key != index - 1)
                    {
                        item.Value.gameObject.SetActive(false);
                    }
                }
                if (currentWeaponIndex == index - 1)
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
        else
        {
            //play deny sound cause already reloading
        }
    }

    public void Shoot()
    {
        if (currentWeaponIndex != -1&&isReloading==false)
        {
            if (Time.time >= nextTimetoFire )
            {
                if (weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet > 0)
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
                    weaponDataSO.Weapons[currentWeaponIndex].SetCurrentUsedBullet(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet - 1);
                    Debug.Log("current Bullets: " + weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet);
                    //check if DamageAble object
                    ToggleShootAnimation(true);
                }
                else
                {
                    ToggleShootAnimation(false);
                    spawnedWeapons[currentWeaponIndex].GetWeaponAnimController().SetBool("isEmpty", true);
                }
                

            }
        }

    }

    public void ReloadCurrentWeapon()
    {
        if (weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet < weaponDataSO.Weapons[currentWeaponIndex].MaxUsedBullet && weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets>0)
        {
            isReloading = true;
            spawnedWeapons[currentWeaponIndex].GetWeaponAnimController().SetBool("isReloading", isReloading);
            StartReloading();
        }
    }

    private async void StartReloading()
    {
        await Task.Delay(weaponDataSO.Weapons[currentWeaponIndex].ReloadTimeInSeconds*1000);
        int neededBullets = weaponDataSO.Weapons[currentWeaponIndex].MaxUsedBullet - weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet;
        if (weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets>=neededBullets)
        {
            weaponDataSO.Weapons[currentWeaponIndex].SetCurrentUsedBullet(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet + neededBullets);
            weaponDataSO.Weapons[currentWeaponIndex].SetCurrentInMagBullet(weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets - neededBullets);
        }
        else
        {
            weaponDataSO.Weapons[currentWeaponIndex].SetCurrentUsedBullet(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet +weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets);
            weaponDataSO.Weapons[currentWeaponIndex].SetCurrentInMagBullet(0);
        }
        isReloading=false;
        spawnedWeapons[currentWeaponIndex].GetWeaponAnimController().SetBool("isReloading", isReloading);
        spawnedWeapons[currentWeaponIndex].GetWeaponAnimController().SetBool("isEmpty", false);
        Debug.Log("After Reload current Bullets: " + weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet);
        //Update in UI later
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