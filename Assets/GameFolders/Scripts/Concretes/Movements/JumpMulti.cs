using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Movements;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class JumpMulti : IJump
    {
        public bool IsJump { get; set; }
        
        private Rigidbody2D _rigidbody2D;
        private IOnGround _onGround;
        
        private float _jumpForce = 350f;
        private int _maxJumpCount = 2;
        private int _currentJumpCount = 0;
        
        public JumpMulti(Rigidbody2D rigidbody2D, IOnGround onGround)
        {
            _rigidbody2D = rigidbody2D;
            _onGround = onGround;
        }
        public void TickWithFixedUpdate()
        {
            if (IsJump)
            {
                if (_onGround.IsGround)
                {
                    _currentJumpCount = 0;
                }
                if (_currentJumpCount < _maxJumpCount)
                {
                    _rigidbody2D.velocity = Vector2.zero;
                    _rigidbody2D.AddForce(Vector2.up * _jumpForce);
                    _currentJumpCount++;
                }
                IsJump = false;
            }
        }
    }
}