using System.Collections.Generic;
using UnityEngine;

namespace MuseumApp
{
    public class HomeScreen : MonoBehaviour
    {
        public RectTransform attractionEntriesParent;

        public AttractionEntryGraphics attractionPrefab;
        public List<AttractionConfig> attractions;

        private void Awake()
        {
            foreach (var attraction in attractions)
            {
                var newAttraction = Instantiate(attractionPrefab, attractionEntriesParent);
                newAttraction.Setup(attraction);
            }
        }
    }
}