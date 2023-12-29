using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Switcher : MonoBehaviour
    {
        [SerializeField] private Button _onButton;
        [SerializeField] private Button _offButton;

        public bool IsOn { get; private set; }
        public event Action<bool> OnSwitch;
        
        private void Start()
        {
            _onButton.onClick.AddListener(() => Switch(true));
            _offButton.onClick.AddListener(() => Switch(false));
        }

        public void Switch(bool isOn, bool invokeEvent = true)
        {
            IsOn = isOn;
            _onButton.interactable = !isOn;
            _offButton.interactable = isOn;
            if (invokeEvent) OnSwitch?.Invoke(IsOn);
        }
    }
}