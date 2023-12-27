using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.NPC;

namespace Game.Objects
{
    public class DeactivatorObject : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private BoxCollider coll;
        private float _deactivatorDelay = 0.8f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Furniture>())
            {
                bool isDeactivate = true;
                for (int i = 0; i < other.transform.childCount; i++)
                {
                    if (other.transform.GetChild(i).GetComponent<Character>())
                    {
                        isDeactivate = false;

                    }
                }

                if (isDeactivate)
                {
                    StartCoroutine(Deactivator(other.gameObject));
                }
            }
        }

        private IEnumerator Deactivator(GameObject fur)
        {
            audioSource.Play();
            fur.SetActive(false);
            coll.enabled = false;
            yield return new WaitForSeconds(_deactivatorDelay);           
            gameObject.SetActive(false);
        }
    }
}

