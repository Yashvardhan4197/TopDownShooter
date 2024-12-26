using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName ="PickupData",menuName ="ScriptableObjects/PickupData")]
public class PickupDataSO: ScriptableObject
{
    [Serializable]
    public class PickupCollection
    {
        [SerializeField] PickupType pickupType;
        [SerializeField] AnimatorController pickupAnimator;
        [SerializeField] float healthBoost;
        [SerializeField] float activatedTimer;
        [SerializeField] int ammoBoost;
        [SerializeField] int destructTime;

        public PickupType PickupType { get { return pickupType; } } 
        public float HealthBoost { get { return healthBoost; } }
        public int AmmoBoost { get { return ammoBoost; } }
        public float ActivatedTimer {  get { return activatedTimer; } }
        public AnimatorController PickupAnimator { get { return pickupAnimator; } }
        public int DestructTime {  get { return destructTime; } }

    }

    [SerializeField] List<PickupCollection> pickupCollection = new List<PickupCollection>();
    public List<PickupCollection> PickupCollections { get { return pickupCollection; } }

}

public enum PickupType
{
    HEALTH_BOOST,
    SHIELD,
    AMMO_BOOST
}