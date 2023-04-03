using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Concretes.Controllers;
using GameFolders.Scripts.Concretes.Managers;
using TMPro;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.UIs
{
    public class QuestionPanel : MonoBehaviour
    {
        [SerializeField] private ResultPanel resultPanel;
        
        private TextMeshProUGUI _messageText;
        private int _lifeCount;
        private IHealth _playerHealth;

        private void Awake()
        {
            _messageText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        private void OnDisable()
        {
            _lifeCount = 0;
            _playerHealth = null;
        }

        public void SetLifeCountAndReferences(int lifeCount, IHealth playerHealth)
        {
            _lifeCount = lifeCount;
            _messageText.text = $"Do you want to buy {_lifeCount} life?";
            _playerHealth = playerHealth;
        }

        public void YesClick()
        {
            resultPanel.gameObject.SetActive(true);

            if (_lifeCount <= GameManager.Instance.Score)
            {
                resultPanel.SetResultMessage($"You have bought {_lifeCount}. Have a good game... ");
                
                GameManager.Instance.DecreaseScore(_lifeCount);

                _playerHealth.Heal(_lifeCount);
            }
            else
            {
                resultPanel.SetResultMessage("You don't have enough diamond. Please try again later...");
            }

            this.gameObject.SetActive(false);
        }
    }
}

