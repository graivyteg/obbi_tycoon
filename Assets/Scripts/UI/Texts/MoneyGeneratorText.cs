using System;
using UnityEngine;
using Zenject;

namespace UI.Texts
{
    public class MoneyGeneratorText : CustomText
    {
        [SerializeField] private string _format = "{0}$ [ +{1} $/сек. ]";
        [Inject] private MoneyGenerator _generator;

        private void Update()
        {
            Text.text = string.Format(_format, _generator.Wallet.Money, Math.Round(_generator.MoneyPerSecond, 1));
        }
    }
}