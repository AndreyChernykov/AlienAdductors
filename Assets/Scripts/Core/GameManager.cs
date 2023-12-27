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

        public Character character { get; set; }
        public int playerSteps { get; private set; }
        public bool isGameOver { get; private set; }

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
            playerSteps = levelManager.StartLevelManager();

            gameUI.DisplayStepsCounter(playerSteps);
            gameUI.DisplayLevelCounter(levelManager.curentLevel);

            isGameOver = false;
        }

        public void StartNewLevel()
        {
            isGameOver = false;

            playerSteps = levelManager.NextLevel();
            gameUI.DisplayLevelCounter(levelManager.curentLevel);            
            gameUI.DisplayStepsCounter(playerSteps);
        }

        private void PlayerStepsCounter()
        {
            if (playerSteps == 0)
            {
                isGameOver = true;
                StartCoroutine(GameOver());
            }
            if (playerSteps > 0)
            {
                playerSteps--;
                gameUI.DisplayStepsCounter(playerSteps);
            }
        }

        public void SetSound(bool b)
        {
            if(b)audioMixerGroup.audioMixer.SetFloat("MasterVolume", 0);
            else audioMixerGroup.audioMixer.SetFloat("MasterVolume", -80);

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
            gameUI.DisplayGameOver();
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

