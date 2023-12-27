using Zenject;

namespace Triggers
{
    public class MoneyGeneratorTrigger : BasePlayerTrigger
    {
        [Inject] private MoneyGenerator _generator;
        
        protected override void OnPlayerStay()
        {
            if (_generator.Wallet.Money > 0)
            {
                _generator.Withdraw(player.Wallet);   
            }
        }
    }
}