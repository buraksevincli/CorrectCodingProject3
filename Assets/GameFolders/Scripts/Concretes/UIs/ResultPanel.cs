using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.UIs
{
    public class ResultPanel : MonoBehaviour
    {
        private TextMeshProUGUI _resultMessage;

        private void Awake()
        {
            _resultMessage = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void SetResultMessage(string result)
        {
            _resultMessage.text = result;
        }
    }

}
