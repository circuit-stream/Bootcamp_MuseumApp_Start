using System;

namespace MuseumApp
{
    [Serializable]
    public class PlayerData
    {
        public static string playerDataSaveKey = "playerDataSaveKey";

        public string username;

        // Don't ever store passwords like this! Just an example!!
        public string password;
    }
}