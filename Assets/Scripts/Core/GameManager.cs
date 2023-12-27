using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.NPC;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField] private GameUI gameUI;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private float delayWindowsDisplay;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        [SerializeField] private PhysicsRaycaster raycaster;
        public bool onExit { get; set; }
        public Character character { get; set; }
        public int playerSteps { get; private set; }

        private float _volumeOff = -80;

        private void OnEnable()
        {
            Main.OneTick += PlayerStepsCounter;
        }

        private void OnDisable()
        {
            Main.OneTick -= PlayerStepsCounter;
        }

        public void StartGame()
        {
            onExit = false;

            playerSteps = levelManager.StartLevelManager();

            gameUI.DisplayStepsCounter(playerSteps);
            gameUI.DisplayLevelCounter(levelManager.curentLevel);
        }

        public void StartNewLevel()
        {
            onExit = false;

            playerSteps = levelManager.NextLevel();
            gameUI.DisplayLevelCounter(levelManager.curentLevel);            
            gameUI.DisplayStepsCounter(playerSteps);
        }

        private void PlayerStepsCounter()
        {
            if (playerSteps > 0)
            {
                SetGameState(GameState.Play);
            }
            if(playerSteps == 0)
            {
                if(onExit) SetGameState(GameState.Win);
                else SetGameState(GameState.Lose);
            }

        }

        public void SetGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Play:
                    playerSteps--;
                    gameUI.DisplayStepsCounter(playerSteps);
                    break;
                case GameState.Win:
                    StartCoroutine(LevelPassed());
                    break;
                case GameState.Lose:
                    StartCoroutine(GameOver());
                    break;
            }
        }

        public void SetSound(bool b)
        {
            if(b)audioMixerGroup.audioMixer.SetFloat("MasterVolume", 0);
            else audioMixerGroup.audioMixer.SetFloat("MasterVolume", _volumeOff);

        }

        public void PlayerWin()
        {
            gameUI.DisplayWin();
        }

        public IEnumerator GameOver()
        {
            raycaster.enabled = false;
            character.SetState(CharacterState.WakeUp);
            yield return new WaitForSeconds(delayWindowsDisplay);
            if (!onExit) gameUI.DisplayGameOver();
            raycaster.enabled = true;
        }

        public IEnumerator LevelPassed()
        {
            raycaster.enabled = false;
            yield return new WaitForSeconds(delayWindowsDisplay);
            if(levelManager.curentLevel < levelManager.totalLevel)
            {
                gameUI.DisplayLevelPassed();
            }
            else
            {
                PlayerWin();
            }
            raycaster.enabled = true;
        }
    }
}

