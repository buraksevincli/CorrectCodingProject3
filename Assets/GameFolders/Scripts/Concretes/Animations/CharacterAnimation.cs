using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Animations
{
    public class CharacterAnimation : IMyAnimation
    {
        private Animator _animator;
        
        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void MoveAnimation(float moveSpeed)
        {
            _animator.SetFloat("moveSpeed", Mathf.Abs(moveSpeed));
        }

        public void JumpAnimation(bool isJump)
        {
            if(_animator.GetBool("isJump") == isJump) return;

            _animator.SetBool("isJump", isJump);
        }

        public void AttackAnimation()
        {
            _animator.SetTrigger("attack");
        }

        public void TakeHitAnimation()
        {
            _animator.SetTrigger("takeHit");
        }

        public void DeadAnimation()
        {
            _animator.SetTrigger("dead");
        }
    }
}

