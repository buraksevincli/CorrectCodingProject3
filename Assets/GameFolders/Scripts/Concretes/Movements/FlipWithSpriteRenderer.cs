using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class FlipWithSpriteRenderer : IFlip
    {
        private SpriteRenderer _spriteRenderer;

        public FlipWithSpriteRenderer(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer.GetComponentInChildren<SpriteRenderer>();
        }
        public void FlipCharacter(float direction)
        {
            if(direction == 0f) return;

            if (direction < 0f)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}

