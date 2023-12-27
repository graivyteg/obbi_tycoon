using UnityEngine;
using Zenject;

namespace UI.Texts
{
    public class MoneyText : CustomText
    {
        [Inject] private Player _player;
        [SerializeField] private string _format = "{0}";

        private void Update()
        {
            Debug.Log(_player);
            Text.text = string.Format(_format, _player.Wallet.Money.ToString());
        }
    }
}