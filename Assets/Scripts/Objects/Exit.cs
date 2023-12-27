using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.NPC;
using Game.Core;

namespace Game.Objects
{
    public class Exit : MonoBehaviour
    {
        [SerializeField] private Transform targetAbduction;

        private GameManager _gameManager;
        private IEnumerator abductions;
        private float _timeDelay = 0.3f;

        private void OnEnable()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).GetComponent<Character>())
                {
                    abductions = Abductions(other.transform.GetChild(i).GetComponent<Character>());
                    StartCoroutine(abductions);

                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).GetComponent<Character>())
                {
                    StopCoroutine(abductions);

                }
            }
        }

        private IEnumerator Abductions(Character character)
        {
            yield return new WaitForSeconds(_timeDelay);
            character.SetAbduction(targetAbduction.position);
            _gameManager.onExit = true;
            _gameManager.SetGameState(GameState.Win);

        }
    }
}

