using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.NPC
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterState characterState;
        [SerializeField] private Animator animator;

        private float _speedDuration = 2f;

        private string _animatorState = "state";

        private void OnEnable()
        {
            SetState(characterState);
        }

        public void SetState(CharacterState state)
        {
            animator.SetInteger(_animatorState, (int)state);

        }

        public void SetAbduction(Vector3 target)
        {
            transform.DOMove(target, _speedDuration, false);
        }
    }
}

