using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuseumApp
{
    public class HomeScreen : MonoBehaviour
    {
        public GameObject loginButton;
        public TMP_Text username;

        public RectTransform attractionEntriesParent;

        public AttractionEntryGraphics attractionPrefab;
        public List<AttractionConfig> attractions;

        public void Signup()
        {
            SceneManager.LoadScene("SignupPopup", LoadSceneMode.Additive);
        }

        private void Awake()
        {
            foreach (var attraction in attractions)
            {
                var newAttraction = Instantiate(attractionPrefab, attractionEntriesParent);
                newAttraction.Setup(attraction);
            }

            SetupUsername();
        }

        public void SetupUsername()
        {
            if (!PlayerPrefs.HasKey(PlayerData.playerDataSaveKey))
            {
                loginButton.SetActive(true);
                username.gameObject.SetActive(false);
                return;
            }

            var playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PlayerData.playerDataSaveKey));

            loginButton.SetActive(false);
            username.gameObject.SetActive(true);

            username.text = playerData.username;
        }
    }
}