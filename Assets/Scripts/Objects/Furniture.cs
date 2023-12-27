using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Core;
using Game.NPC;
using System;
using DG.Tweening;

namespace Game.Objects
{
    public class Furniture : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Moving moving;
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool wakesUpObject;
        [SerializeField] private AudioClip boom;
        [SerializeField] private AudioClip rustle;
        [SerializeField] private AudioClip wakesUp;
        [SerializeField] private AudioSource audioSource;

        private string _axisX = "Mouse X";
        private string _axisZ = "Mouse Y";

        private bool _isMoving = true;


        private Vector3 _lastPosition;
        private GameManager _gameManager;

        IEnumerator _mover;

        private enum Moving
        {
            MovingX,
            MovingZ,
        }

        private void OnEnable()
        {
            _lastPosition = transform.position;
            _gameManager = FindObjectOfType<GameManager>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Character>())
                {
                    _gameManager.character = transform.GetChild(i).GetComponent<Character>(); 
                }
            }

            rb.isKinematic = true;
        }


        //public void OnDrag(PointerEventData eventData)//физическое движение
        //{
        //    Vector3 axis;
        //    rb.isKinematic = false;
        //    if (moving == Moving.MovingX)
        //    {
        //        axis = new Vector3(Input.GetAxis(_axisX), 0, 0);
        //    }
        //    else
        //    {
        //        axis = new Vector3(0, 0, Input.GetAxis(_axisZ));
        //    }
        //    rb.velocity = axis * speed;
        //}

        public void OnDrag(PointerEventData eventData)//не физическое движение
        {
            if (_isMoving)
            {
                if (Math.Abs(Input.GetAxis(_axisZ)) == Math.Abs(Input.GetAxis(_axisX))) return;
                _lastPosition = transform.position;
                if (moving == Moving.MovingX)
                {
                    if (Math.Abs(Input.GetAxis(_axisZ)) > Math.Abs(Input.GetAxis(_axisX))) return;
                    _mover = Mover(new Vector3(transform.position.x + Math.Sign(Input.GetAxis(_axisX)), transform.position.y, transform.position.z));
                }
                else
                {
                    if (Math.Abs(Input.GetAxis(_axisZ)) < Math.Abs(Input.GetAxis(_axisX))) return;
                    _mover = Mover(new Vector3(transform.position.x, transform.position.y, transform.position.z + Math.Sign(Input.GetAxis(_axisZ))));
                }
                audioSource.PlayOneShot(rustle);
                StartCoroutine(_mover);
                _isMoving = false;

                rb.isKinematic = false;
            }

        }

        private IEnumerator Mover(Vector3 targetPos)
        {
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
                yield return new WaitForFixedUpdate();

            }
            Main.Tick();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isMoving = true;
            _lastPosition = transform.position;           
            
            rb.isKinematic = true;

        }

        private void OnCollisionEnter(Collision collision)
        {
            audioSource.PlayOneShot(boom);
            transform.position = _lastPosition;
            if (_mover != null)
            {
                StopCoroutine(_mover);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (wakesUpObject)
            {
                for (int i = 0; i < other.transform.childCount; i++)
                {
                    if (other.transform.GetChild(i).GetComponent<Character>())
                    {
                        audioSource.PlayOneShot(wakesUp);
                        StartCoroutine(_gameManager.GameOver());
                    }
                }
            }
        }
    }
}

