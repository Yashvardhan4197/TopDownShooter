
using System;
using System.Collections.Generic;
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
        [SerializeField] float fireRate;
        [SerializeField] int maxUsedBullet;
        [SerializeField] int maxInMagBullets;
        [SerializeField] int maxRange;
        [SerializeField] WeaponBody weaponBody;
        [SerializeField] TrailRenderer bulletTrail;
        public string WeaponName { get { return weaponName; } }
        public float FireRate { get {  return fireRate; } }
        public int MaxUsedBullet { get {  return maxUsedBullet; } }
        public int MaxInMagBullets { get {  return maxInMagBullets; } }
        public int CurrentUsedBullet { get { return currentUsedBullet; } }
        public int CurrentInMagBullets { get { return currentInMagBullets; } }
        public int MaxRange { get { return maxRange; } }
        public WeaponBody WeaponBody { get { return weaponBody; } }
        public TrailRenderer BulletTrail { get { return bulletTrail; } }

        public void SetCurrentUsedBullet(int currentUsedBullet)
        {
            this.currentUsedBullet=currentUsedBullet;
        }

        public void SetCurrentInMagBullet(int currentInMagBullet)
        {
            this.currentInMagBullets=currentInMagBullet;
        }
    }

    [SerializeField] List<WeaponDataCollection> weapons=new List<WeaponDataCollection>();

    public List<WeaponDataCollection> Weapons {  get { return weapons; } }

}