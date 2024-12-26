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
        GameService.Instance.StartGameAction += OnGameStart;
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

                GameService.Instance.UIService.GetWeaponUIController().SetCurrentWeapon(currentWeaponIndex);
                if(currentWeaponIndex!=-1)
                {
                    GameService.Instance.UIService.GetWeaponUIController().UpdateCurrentBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet);
                    GameService.Instance.UIService.GetWeaponUIController().UpdateTotalBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets);
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
        if (currentWeaponIndex != -1 && isReloading==false)
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
                    int layerMask = ~LayerMask.GetMask("IgnoreTrigger");
                    hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, weaponDataSO.Weapons[currentWeaponIndex].MaxRange,layerMask);
                    InstantiateBulletTracer(ray2D, hit2D);
                    weaponDataSO.Weapons[currentWeaponIndex].SetCurrentUsedBullet(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet - 1);
                    GameService.Instance.UIService.GetWeaponUIController().UpdateCurrentBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet);
                    IDamageAble damageAbleItem = hit2D.transform?.GetComponent<IDamageAble>();

                    if (damageAbleItem != null)
                    {
                        damageAbleItem.TakeDamage(weaponDataSO.Weapons[currentWeaponIndex].Damage);
                        Debug.Log("Attacked");
                    }
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

    private void InstantiateBulletTracer(Ray2D ray2D, RaycastHit2D hit2D)
    {
        var tracer = UnityEngine.Object.Instantiate(weaponDataSO.Weapons[currentWeaponIndex].BulletTrail, ray2D.origin, Quaternion.identity);
        tracer.AddPosition(ray2D.origin);
        Vector2 targetPos = Vector2.zero;
        if(hit2D.transform!=null)
        {
            targetPos = hit2D.point;
        }
        else
        {
            targetPos=ray2D.origin+ray2D.direction*weaponDataSO.Weapons[currentWeaponIndex].MaxRange;
        }
        tracer.transform.position = targetPos;
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
        GameService.Instance.UIService.GetWeaponUIController().UpdateCurrentBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentUsedBullet);
        GameService.Instance.UIService.GetWeaponUIController().UpdateTotalBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets);
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

    public void SetPlayerDirection(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        float x= Mathf.Cos(angle*Mathf.Deg2Rad);
        float y=Mathf.Sin(angle*Mathf.Deg2Rad);

        int xDirection = 0;
        int yDirection = 0;

        if (Mathf.Abs(x) > 0.5f)
        {
            xDirection = x > 0 ? 1 : -1;
        }

        if (Mathf.Abs(y) > 0.5f)
        {
            yDirection = y > 0 ? 1 : -1;
        }

        GameService.Instance.PlayerService.GetPlayerController().SetPlayerDirection(xDirection, yDirection);
    }


    public void IncreaseAmmo(int ammo)
    {
        for (int i= 0;i < weaponDataSO.Weapons.Count;i++)
        {
            if (weaponDataSO.Weapons[i].CurrentInMagBullets < weaponDataSO.Weapons[i].MaxInMagBullets)
            {
                int temp = ammo + weaponDataSO.Weapons[i].CurrentInMagBullets;
                if(temp>= weaponDataSO.Weapons[i].MaxInMagBullets)
                {
                    temp = weaponDataSO.Weapons[i].MaxInMagBullets;
                }
                weaponDataSO.Weapons[i].SetCurrentInMagBullet(temp);
            }
        }
        if (currentWeaponIndex != -1)
        {
            GameService.Instance.UIService.GetWeaponUIController().UpdateTotalBullets(weaponDataSO.Weapons[currentWeaponIndex].CurrentInMagBullets);
        }
    }

}