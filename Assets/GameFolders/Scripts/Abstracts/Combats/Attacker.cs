using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Combats;
using UnityEngine;

namespace GameFolders.Scripts.Abstracts.Combats
{
    public abstract class Attacker : MonoBehaviour, IAttacker
    {
        [SerializeField] private int damage = 1;
        
        public int Damage => damage;
        
        public virtual void Attack(ITakeHit takeHit)
        {
            takeHit.TakeHit(this);
        }
    }
}

