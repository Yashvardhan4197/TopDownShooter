
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponData",menuName ="ScriptableObjects/WeaponData")]
public class WeaponDataSO: ScriptableObject
{
    [Serializable]
    public class WeaponDataCollection
    {
        private int currentInMagBullets;
        private int currentUsedBullet;
        [SerializeField] string weaponName;
        [SerializeField] Sprite weaponSprite;
        [SerializeField] int fireRate;
        [SerializeField] int maxUsedBullet;
        [SerializeField] int maxInMagBullets;

        public string WeaponName { get { return weaponName; } }
        public Sprite WeaponSprite { get {  return weaponSprite; } }
        public int FireRate { get {  return fireRate; } }
        public int MaxUsedBullet { get {  return maxUsedBullet; } }
        public int MaxInMagBullets { get {  return maxInMagBullets; } }
        public int CurrentUsedBullet { get { return currentUsedBullet; } }
        public int CurrentInMagBullets { get { return currentInMagBullets; } }

        public void SetCurrentUsedBullet(int currentUsedBullet)
        {
            this.currentUsedBullet=currentUsedBullet;
        }

        public void SetCurrentInMagBullet(int currentInMagBullet)
        {
            this.currentInMagBullets=currentInMagBullet;
        }
    }

    [SerializeField] WeaponDataCollection[] weapons;

}