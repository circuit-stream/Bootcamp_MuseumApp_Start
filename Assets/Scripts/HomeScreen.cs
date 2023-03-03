using System.Collections.Generic;
using System.Linq;
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
        public List<AttractionEntryGraphics> attractionEntries;

        private List<string> enabledAttractions;

        public void Signup()
        {
            SceneManager.LoadScene("SignupPopup", LoadSceneMode.Additive);
        }

        public void LogOff()
        {
            // LogOff
            User.LogOff();

            Refresh();
        }

        public void Refresh()
        {
            SetupUsername();

            foreach (var attraction in attractionEntries)
                attraction.Refresh(IsAttractionEnabled(attraction));
        }

        private void Awake()
        {
            SetEnabledAttractions();
            SetUpAttractions();
            SetupUsername();

            PlayfabController.Instance.titleDataAquired += OnTitleDataFetched;
            PlayfabController.Instance.LoginWithPlayfab(PlayfabController.Instance.FetchTitleData);


        }

        private void OnTitleDataFetched()
        {
            SetEnabledAttractions();
            Refresh();
        }

        private void SetupUsername()
        {
            // TODO
            if (!User.IsLoggedIn)
            {
                loginButton.SetActive(true);
                username.gameObject.SetActive(false);
                return;
            }

            loginButton.SetActive(false);
            username.gameObject.SetActive(true);

            // username.text = <NAME>;
            username.text = User.LoggedInUsername;
        }

        private void SetUpAttractions()
        {
            attractionEntries = new List<AttractionEntryGraphics>(attractions.Count);
            foreach (var attraction in attractions)
            {
                var newAttraction = Instantiate(attractionPrefab, attractionEntriesParent);
                newAttraction.Setup(attraction);
                attractionEntries.Add(newAttraction);
            }
        }

        private bool IsAttractionEnabled(AttractionEntryGraphics attraction)
        {
            return enabledAttractions != null && enabledAttractions.Contains(attraction.Id);
        }

        private void SetEnabledAttractions()
        {
            if(PlayfabController.Instance.titleData == null)
            {
                return;
            }
            enabledAttractions = PlayfabController
                .Instance
                .titleData["EnabledAttractions"]
                .Split(',')
                .ToList();
        }

        public void DeleteUser()
        {
            if (!User.IsLoggedIn)
            {
                loginButton.SetActive(true);
                username.gameObject.SetActive(false);
                return;
            }

            Database.DeleteUser(User.LoggedInUsername);
            LogOff();
        }
    }
}