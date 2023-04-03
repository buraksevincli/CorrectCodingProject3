using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.UIs
{
    public class ShopGameObject : MonoBehaviour
    {
        [SerializeField] private QuestionPanel questionPanel;
        [SerializeField] private GameObject shop;

        private IHealth _playerHealth;
        
        private void OnEnable()
        {
            _playerHealth = FindObjectOfType<PlayerController>().GetComponent<IHealth>();
        }

        private void OnDisable()
        {
            _playerHealth = null;
        }

        public void BuyLifeClick(int lifeCount)
        {
            questionPanel.gameObject.SetActive(true);
            questionPanel.SetLifeCountAndReferences(lifeCount, _playerHealth);
        }

        public void IsActiveShop(bool isActive)
        {
            shop.SetActive(isActive);
        }
    }

}
