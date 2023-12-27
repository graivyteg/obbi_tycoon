using System;
using FIMSpace;
using NaughtyAttributes;
using UnityEngine;

namespace UI.Texts
{
    public class BuildingTriggerText : CustomText
    {
        [SerializeField] private string _format = "{0} <color={1}>[ {2}$ ]</color>\n+{3}/с.";
        [Foldout("Advanced")]
        [SerializeField] private string _availableColor = "green";
        [Foldout("Advanced")]
        [SerializeField] private string _unavailableColor = "red";

        private Building _building;

        protected override void Start()
        {
            base.Start();
            
            _building = GetComponentInParent<Building>();
            if (_building == null) Debug.LogError($"There is no building in parent for text {name}");
        }
        
        private void Update()
        {
            var color = _building.IsMoneyEnough() ? _availableColor : _unavailableColor;
            Text.text = string.Format(_format, _building.Name, color, _building.Price, _building.Bonus);
        }
    }
}