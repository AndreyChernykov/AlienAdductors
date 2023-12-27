using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Core
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private TextMeshProUGUI textStepCounter;
        [SerializeField] private TextMeshProUGUI textNumLevel;
        [SerializeField] private TextMeshProUGUI textButtonSound;
        [SerializeField] private GameObject windowGameOver;
        [SerializeField] private GameObject windowLevelPassed;
        [SerializeField] private GameObject windowMenu;
        [SerializeField] private GameObject windowWin;

        private List<GameObject> _windowList = new List<GameObject>();

        private string _textSteps = "steps: ";
        private string _textNumLvl = "level: ";
        private string _textSoundOn = "sound on";
        private string _textSoundOff = "sound off";

        private bool _isSoindOn = false;

        private void OnEnable()
        {
            _windowList.Add(windowGameOver);
            _windowList.Add(windowLevelPassed);
            _windowList.Add(windowMenu);
            _windowList.Add(windowWin);

            WindowActivate(windowMenu);
            ClickSound();
        }

        private void WindowActivate(GameObject window)
        {
            foreach (var w in _windowList)
            {
                w.SetActive(false);
            }
            if(window != null)window.SetActive(true);
        }

        public void DisplayStepsCounter(int steps)
        {
            textStepCounter.text = _textSteps + steps;
        }

        public void DisplayLevelCounter(int lvl)
        {
            textNumLevel.text = _textNumLvl + (lvl + 1);
        }

        public void DisplayGameOver()
        {
            WindowActivate(windowGameOver);
        }

        public void DisplayLevelPassed()
        {
            WindowActivate(windowLevelPassed);
        }

        public void DisplayWin()
        {
            WindowActivate(windowWin);
        }

        public void ClickStartGame()
        {
            gameManager.StartGame();
            WindowActivate(null);
        }

        public void ClickNextLevel()
        {
            gameManager.StartNewLevel();
            WindowActivate(null);
        }

        public void ClickMenu()
        {
            WindowActivate(windowMenu);
        }

        public void ClickSound()
        {
            if (_isSoindOn)
            {
                textButtonSound.text = _textSoundOff;
                _isSoindOn = false;
            }
            else
            {
                textButtonSound.text = _textSoundOn;
                _isSoindOn = true;
            }

            gameManager.SetSound(_isSoindOn);
        }

        public void ClickQuitGame()
        {
            Application.Quit();
        }
    }
}

