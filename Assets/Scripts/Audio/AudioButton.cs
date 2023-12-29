using Audio;
using UI.Buttons;

namespace Audio
{
    public class AudioButton : CustomButton
    {
        protected override void OnClick()
        {
            AudioButtonSource.Instance.PlayClickSound();
        }
    }
}