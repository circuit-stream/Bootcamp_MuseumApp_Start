using UnityEngine;

namespace MuseumApp
{
    public class AttractionScreenParameters : MonoBehaviour
    {
        public AttractionConfig attractionConfig;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}