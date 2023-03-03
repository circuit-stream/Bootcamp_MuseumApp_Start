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

        public string Id => attractionConfig.id;

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

            
        }

        public void Refresh(bool isAttractionEnabled)
        {
            // StarsRatingLib.SetupStars
            StarsRatingLib.SetUpStars(stars, attractionConfig.id);
            gameObject.SetActive(isAttractionEnabled);
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