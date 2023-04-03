using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class MoverWithVelocity : IMover
    {
        private Rigidbody2D _rigidbody2D;

        private float MoveSpeed = 90f;
        
        public MoverWithVelocity(PlayerController playerController)
        {
            _rigidbody2D = playerController.GetComponent<Rigidbody2D>();
        }
        public void Tick(float horizontal)
        {
            if(_rigidbody2D.velocity.magnitude >= 5f) return;
            
            Vector2 direction = new Vector2(horizontal, _rigidbody2D.velocity.y);
            Vector2 movement = direction.normalized * (Time.fixedDeltaTime * MoveSpeed);

            _rigidbody2D.velocity = movement;
        }
    }
}

