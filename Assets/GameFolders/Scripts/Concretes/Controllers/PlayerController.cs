using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.Inputs;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Concretes.Animations;
using GameFolders.Scripts.Concretes.Inputs;
using GameFolders.Scripts.Concretes.Managers;
using GameFolders.Scripts.Concretes.Movements;
using GameFolders.Scripts.Concretes.UIs;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] private float moveSpeed = 3f;
        
        private IPlayerInput _playerInput;
        private IMover _mover;
        private IMyAnimation _animation;
        private IFlip _flip;
        private IOnGround _onGround;
        private IJump _jump;
        private IHealth _health;

        private float _horizontal;

        private void Awake()
        {
            _playerInput = new PcInput();
            _mover = new MoverWithTranslate(this, moveSpeed);
            _animation = new CharacterAnimation(GetComponent<Animator>());
            _flip = new FlipWithTransform(this);
            _onGround = GetComponent<IOnGround>();
            _jump = new JumpMulti(GetComponent<Rigidbody2D>(),_onGround);
            _health = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            _health.OnDead += _animation.DeadAnimation;
            _health.OnDead += GameManager.Instance.SaveScore;
        }

        private void Start()
        {
            GameOverObject gameOverObject = FindObjectOfType<GameOverObject>();
            gameOverObject.SetPlayerHealth(_health);
        }

        private void FixedUpdate()
        {
            _flip.FlipCharacter(_horizontal);
            
            _mover.Tick(_horizontal);
            
            _jump.TickWithFixedUpdate();
        }
        
        private void Update()
        {
            if(_health.IsDead) return;
            
            _horizontal = _playerInput.Horizontal;

            if (_playerInput.AttackButtonDown && _horizontal == 0f)
            {
                _animation.AttackAnimation();
                return;
            }
            
            if (_playerInput.JumpButtonDown)
            {
                _jump.IsJump = true;
            }
            
            _animation.JumpAnimation(!_onGround.IsGround);
            _animation.MoveAnimation(_horizontal);
        }
        
    }
}

