using UnityEngine;

namespace MuseumApp
{
    [CreateAssetMenu(fileName = "New Attraction Config", menuName = "Configs/AttractionConfig", order = 0)]
    public class AttractionConfig : ScriptableObject
    {
        public string id;
        public string title;
        public string author;
        public string description;
        public string location;
        public float latitude;
        public float longitude;

        public Sprite image;

        public Vector2 thumbnailSize;
        public Vector3 thumbnailPosition;

        public Vector2 headerImageSize;
        public Vector3 headerImagePosition;
    }
}