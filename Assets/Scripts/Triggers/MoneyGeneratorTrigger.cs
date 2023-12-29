using Audio;
using Zenject;

namespace Triggers
{
    public class MoneyGeneratorTrigger : BasePlayerTrigger
    {
        [Inject] private MoneyGenerator _generator;
        [Inject] private SoundController _soundController;
        
        protected override void OnPlayerStay()
        {
            if (_generator.Wallet.Money > 0)
            {
                _soundController.PlaySound(SoundController.SoundType.Cash);
                _generator.Withdraw(player.Wallet);   
            }
        }
    }
}