using System;
using NaughtyAttributes;
using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(Renderer))]
    public class BuildingTrigger : BasePlayerTrigger
    {
        [Foldout("Advanced")]
        [SerializeField] private float _delay = 0.3f;
        
        private bool _isPlayerInside = false;
        private float _timer = 0;

        private Building _building;

        public void Initialize(Building building)
        {
            _building = building;
            
            if (_building.IsBuilt) OnBuild();
            else _building.OnBuild += OnBuild;
        }

        private void OnDisable()
        {
            if (_building == null) return;
            
            _building.OnBuild -= OnBuild;
        }

        private void Update()
        {
            if (_building == null) return;
            if (!_isPlayerInside)
            {
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
            if (_timer < _delay) return;

            if (_building.TryBuild()) OnBuild();
        }

        private void OnBuild()
        {
            gameObject.SetActive(false);
        }
        
        protected override void OnPlayerEnter()
        {
            _isPlayerInside = true;
        }

        protected override void OnPlayerExit()
        {
            _isPlayerInside = false;
        }
    }
}