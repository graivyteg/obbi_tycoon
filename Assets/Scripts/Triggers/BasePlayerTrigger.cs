using System;
using UnityEngine;
using Zenject;

namespace Triggers
{
    [RequireComponent(typeof(Renderer))]
    public class BasePlayerTrigger : MonoBehaviour
    {
        [Inject] protected Player player;

        private void OnTriggerEnter(Collider other)
        {
            CheckCollider(other, OnPlayerEnter);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckCollider(other, OnPlayerExit);
        }

        private void OnTriggerStay(Collider other)
        { 
            CheckCollider(other, OnPlayerStay);
        }

        private void CheckCollider(Collider collider, Action successCallback)
        {
            if (collider.gameObject == player.gameObject) successCallback.Invoke();
        }
        
        protected virtual void OnPlayerEnter() { }
        protected virtual void OnPlayerExit() { }
        protected virtual void OnPlayerStay() { }
    }
}