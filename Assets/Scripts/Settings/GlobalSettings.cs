using NUnit.Framework;
using UnityEngine;
using YG;

namespace Settings
{
    [CreateAssetMenu(fileName = "Global Settings", menuName = "Game/Global Settings", order = 51)]
    public class GlobalSettings : ScriptableObject
    {
        public bool ForceMobile = false;
    }
}