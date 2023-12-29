using System;
using Triggers;
using UnityEngine;

namespace UI
{
    public class AdvertisementTriggerText : CustomText
    {
        [SerializeField] private string _format= "+{0}$";

        private AdvertisementTrigger _trigger;
        
        protected override void Start()
        {
            base.Start();
            _trigger = GetComponentInParent<AdvertisementTrigger>();
        }

        private void Update()
        {
            Text.text = string.Format(_format, _trigger.GetRewardAmount());
        }
    }
}