
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController
{
    private WeaponUIView weaponUIView;
    private int currentWeaponBullets;
    private int currentWeaponTotalBullets;
    public WeaponUIController(WeaponUIView weaponUIView)
    {
        this.weaponUIView = weaponUIView;
        this.weaponUIView.SetController(this);
        GameService.Instance.StartGameAction += OnGameStart;
    }

    public void OnGameStart()
    {
        currentWeaponBullets = 0;
        currentWeaponTotalBullets = 0;
        UpdateBulletsOnView();
        DeactivateText();
    }


    public void UpdateTotalBullets(int bullets)
    {
        currentWeaponTotalBullets=bullets;
        UpdateBulletsOnView();
    }

    public void UpdateCurrentBullets(int bullets)
    {
        currentWeaponBullets=bullets;
        UpdateBulletsOnView();
    }

    private void UpdateBulletsOnView()
    {
        weaponUIView.GetCurrentWeaponBullets().text=currentWeaponBullets.ToString()+"/"+currentWeaponTotalBullets.ToString();
        
    }

    public void ActivateText()
    {
        weaponUIView.GetCurrentWeaponBullets().gameObject.SetActive(true);
    }

    public void DeactivateText()
    {
        weaponUIView.GetCurrentWeaponBullets().gameObject.SetActive(false);
    }


    public void SetCurrentWeapon(int index)
    {
        Image[] collection = weaponUIView.GetWeaponImageCollection();
        for(int i=0;i<collection.Length;i++)
        {
            collection[i].color = Color.white;
        }
            
        if(index!=-1)
        {
            collection[index].color = Color.gray;
            ActivateText();
        }
        else
        {
            currentWeaponBullets = 0;
            currentWeaponTotalBullets = 0;
            DeactivateText();
        }
    }

}
