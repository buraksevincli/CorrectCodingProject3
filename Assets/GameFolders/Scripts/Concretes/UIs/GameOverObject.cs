using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Combats;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.UIs
{
    public class GameOverObject : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private IHealth _playerHealth;

        private void OnEnable()
        {
            gameOverPanel.SetActive(false);
        }

        private void HandleDead()
        {
            gameOverPanel.SetActive(true);
            _playerHealth.OnDead -= HandleDead;
            _playerHealth = null;
        }

        public void SetPlayerHealth(IHealth health)
        {
            _playerHealth = health;
            _playerHealth.OnDead += HandleDead;
        }
    }

}
