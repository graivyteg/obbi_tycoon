using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Debug Settings", menuName = "Game/Debug Settings", order = 51)]
    public class DebugSettings : ScriptableObject
    {
        public bool IsDebugMode = false;
        public bool IsMobile = false;
    }
}