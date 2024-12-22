using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName ="ScriptableObjects/EnemyData")]
public class EnemyDataSO:ScriptableObject
{
    [Serializable]
    public class EnemyCollection
    {
        [SerializeField] float health;
        [SerializeField] float damage;
        [SerializeField] float movementSpeed;
        [SerializeField] float attackDelay;
        [SerializeField] AnimatorController animatorController;
        public float Health { get { return health; } }
        public float Damage { get { return damage; } }
        public float AttackDelay { get { return attackDelay; } }
        public float MovementSpeed { get { return movementSpeed; } }
        public AnimatorController AnimatorController { get { return animatorController; } }
    }

    [SerializeField] List<EnemyCollection> enemyCollections=new List<EnemyCollection>();
    public List<EnemyCollection> EnemyCollections { get { return enemyCollections; } }

}