using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Concretes.UIs;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class ShopController : MonoBehaviour
    {
        private ShopGameObject _shopGameObject;
        
        private void Start()
        {
            _shopGameObject = FindObjectOfType<ShopGameObject>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IsPlayerTriggered(col, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsPlayerTriggered(other, false);
        }

        private void IsPlayerTriggered(Collider2D collider2D, bool isActive)
        {
            PlayerController playerController = collider2D.GetComponent<PlayerController>();

            if (playerController != null)
            {
                _shopGameObject.IsActiveShop(isActive);
            }
        }
    }
}

