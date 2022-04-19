using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuseumApp
{
    public class AttractionEntryGraphics : MonoBehaviour
    {
        public Image thumbnail;

        public TMP_Text attractionTitle;
        public TMP_Text attractionLocation;

        public Image[] stars;

        public AttractionScreenParameters screenParametersPrefab;

        private AttractionConfig attractionConfig;

        public void OnClick()
        {
            var parametersObject = Instantiate(screenParametersPrefab);
            parametersObject.attractionConfig = attractionConfig;

            SceneManager.LoadScene("AttractionScreen", LoadSceneMode.Single);
        }

        public void Setup(AttractionConfig config)
        {
            attractionConfig = config;

            attractionTitle.text = attractionConfig.title;
            attractionLocation.text = attractionConfig.location;

            SetupThumbnail();
            SetupStars(PlayerPrefs.GetInt(attractionConfig.id));
        }

        private void SetupThumbnail()
        {
            thumbnail.sprite = attractionConfig.image;

            var rectTransform = thumbnail.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = attractionConfig.thumbnailPosition;
            rectTransform.sizeDelta = attractionConfig.thumbnailSize;
        }
    }
}