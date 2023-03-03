using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace MuseumApp
{
    public class AttractionScreen : MonoBehaviour
    {
        private static string weatherAPIEndpoint = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
        private static string weatherAPIKey = "e185c6f8c32910f19e8f29f72864c618";
        [Serializable]
        public class WeatherIconEquivalency
        {
            public Sprite icon;
            public string iconId;
        }

        public Image cover;

        public TMP_Text attractionTitle;
        public TMP_Text attractionLocation;
        public TMP_Text attractionAuthor;
        public TMP_Text attractionDescription;
        public Image weatherIconImage;

        public List<WeatherIconEquivalency> weatherIcons;

        public Image[] stars;

        private AttractionScreenParameters attractionParameters;
        private AttractionConfig attractionConfig;

        public void OnClickBack()
        {
            Destroy(attractionParameters.gameObject);

            SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
        }

        public void OnClickStar(int index)
        {
            // Check if user is logged in
            if (!User.IsLoggedIn)
            {
                return;
            }
            // Create save user rating
            var attractionID = attractionConfig.id;

            Database.Rate(attractionID, index);

            StarsRatingLib.SetUpStars(stars, attractionID);

            PlayfabController.Instance.OnAttractionRated(attractionID, index);
        }

        private void Start()
        {
            attractionParameters = FindObjectOfType<AttractionScreenParameters>();
            attractionConfig = attractionParameters.attractionConfig;

            attractionTitle.text = attractionConfig.title;
            attractionLocation.text = attractionConfig.location;
            attractionAuthor.text = attractionConfig.author;
            attractionDescription.text = attractionConfig.description;

            SetupCover();

            // StarsRatingLib.SetupStars
            StarsRatingLib.SetUpStars(stars, attractionConfig.id);

            // set weather
            weatherIconImage.gameObject.SetActive(false);
            StartCoroutine(SetWeatherIcon());
        }

        private void SetupCover()
        {
            cover.sprite = attractionConfig.image;

            var rectTransform = cover.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = attractionConfig.headerImagePosition;
            rectTransform.sizeDelta = attractionConfig.headerImageSize;
        }

        private IEnumerator SetWeatherIcon()
        {
            string url = string.Format(weatherAPIEndpoint, attractionConfig.latitude, attractionConfig.longitude, weatherAPIKey);
            UnityWebRequest webRequest = UnityWebRequest.Get(url);

            yield return webRequest.SendWebRequest();

            if(webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("weather request network error!");
                yield break;
            }

            WeatherData data = JsonUtility.FromJson<WeatherData>(webRequest.downloadHandler.text);
            Debug.Log(webRequest.downloadHandler.text);
            weatherIconImage.sprite = weatherIcons.Find(x => x.iconId == data.weather[0].icon).icon;
            weatherIconImage.gameObject.SetActive(true);
        }
    }
}