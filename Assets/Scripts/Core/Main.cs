using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Main : MonoBehaviour
    {
        public static event Action UpdateTick;
        public static event Action LateUpdateTick;
        public static event Action FixedUpdateTick;
        public static event Action OneTick;

        [SerializeField] private GameManager gameManager;

        private void Start()
        {
            //gameManager.StartGame();
            
        }

        public static void Tick()
        {
            OneTick?.Invoke();
        }

        private void Update()
        {
            UpdateTick?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateTick?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateTick?.Invoke();
        }
    }
}

