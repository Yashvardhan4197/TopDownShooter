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
        [SerializeField] EnemyType enemyType;
        [SerializeField] AnimatorController animatorController;
        [SerializeField] float attackRadius;
        public float Health { get { return health; } }
        public float Damage { get { return damage; } }
        public float AttackDelay { get { return attackDelay; } }
        public float MovementSpeed { get { return movementSpeed; } }
        public AnimatorController AnimatorController { get { return animatorController; } }
        public EnemyType EnemyType { get { return enemyType; } }
        public float AttackRadius {  get { return attackRadius; } }
    }

    [SerializeField] List<EnemyCollection> enemyCollections=new List<EnemyCollection>();
    public List<EnemyCollection> EnemyCollections { get { return enemyCollections; } }

}