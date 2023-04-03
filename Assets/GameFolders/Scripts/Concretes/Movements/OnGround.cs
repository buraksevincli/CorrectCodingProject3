using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Movements;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class OnGround : MonoBehaviour, IOnGround
    {
        [SerializeField] private Transform[] _transforms;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask _layerMask;

        private bool _isGround;

        public bool IsGround => _isGround;
        private void Update()
        {
            foreach (Transform footTransform in _transforms)
            {
                CheckIfFootsOnGround(footTransform);
                
                if(_isGround) break;
            }
        }

        private void CheckIfFootsOnGround(Transform footTransform)
        {
            RaycastHit2D hit =
                Physics2D.Raycast(footTransform.position, footTransform.forward, maxDistance, _layerMask);

            Debug.DrawRay(footTransform.position, footTransform.forward * maxDistance, Color.red);

            if (hit.collider != null)
            {
                _isGround = true;
            }
            else
            {
                _isGround = false;
            }
        }
    }
}

