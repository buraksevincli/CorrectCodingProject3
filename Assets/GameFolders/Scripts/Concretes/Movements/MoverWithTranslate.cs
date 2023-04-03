using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class MoverWithTranslate : IMover
    {
        private IEntityController _entityController;

        private float _moveSpeed;

        public MoverWithTranslate(IEntityController entityController, float moveSpeed)
        {
            _entityController = entityController;
            _moveSpeed = moveSpeed;
        }
        public void Tick(float horizontal)
        {
            if(horizontal == 0f) return;
            
            _entityController.transform.Translate(Vector2.right * (horizontal * Time.deltaTime * _moveSpeed));
        }
    }
}

