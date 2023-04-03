using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace GameFolders.Scripts.Concretes.UIs
{
    public class DisplayHealth : MonoBehaviour
    {
        private Image _healthImage;
        private IHealth _health;

        private void Awake()
        {
            _healthImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _health = FindObjectOfType<PlayerController>().GetComponent<IHealth>();
            _health.OnHealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            _health.OnHealthChanged -= HandleHealthChanged;
            _healthImage.fillAmount = 1f;
        }

        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
            float result = (float)currentHealth / (float)maxHealth;
            _healthImage.fillAmount = result;
        }
    }
}

