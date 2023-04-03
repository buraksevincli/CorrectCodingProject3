using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Inputs;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Inputs
{
    public class MobileInput : IPlayerInput
    {
        public float Horizontal => Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");
        public bool JumpButtonDown => Input.GetButtonDown("Jump");
        public bool AttackButtonDown => Input.GetButtonDown("Fire1");
    }
}

