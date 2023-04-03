using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.Movements;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class Jump : IJump
    {
        private float _jumpForce = 350f;

        public bool IsJump { get; set; }
        
        private Rigidbody2D _rigidbody2D;
        private IOnGround _onGround;
        
        public Jump(Rigidbody2D rigidbody2D, IOnGround onGround)
        {
            _rigidbody2D = rigidbody2D;
            _onGround = onGround;
        }

        public void TickWithFixedUpdate()
        {
            if (IsJump && _onGround.IsGround )
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            }
            IsJump = false;
        }
    }
}
