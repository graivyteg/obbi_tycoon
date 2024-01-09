
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int level = 0;
        public List<string> buildings = new List<string>();

        public float sensitivityMultiplier = 1;
        public bool isMusicOn = true;
        public bool isSoundOn = true;
        public bool areCitizensOn = true;
        public bool isDaytimeOn = true;
        public bool tutorialCompleted = false;

        public int Record = 0;

        public SavesYG()
        {
        }
    }
}
