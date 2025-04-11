using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftgamesAssignment.Home
{
    public class Carousel : MonoBehaviour
    {
        [System.Serializable]
        private class Entry
        {
            public Sprite sprite;
            public string sceneName;
        }

        [SerializeField] private List<Entry> entries;
        [SerializeField] private Button buttonPageNext;
        [SerializeField] private Button buttonPagePrevious;
        [SerializeField] private GameObject pagePrefab;
        [SerializeField] private GameObject pagesContainer;
        [SerializeField] private Button confirmButton;

        private readonly List<GameObject> entryInstances = new();
        private int currentPage;

        void Start()
        {
            buttonPageNext.onClick.AddListener(GoToNextPage);
            buttonPagePrevious.onClick.AddListener(GoToPreviousPage);
            confirmButton.onClick.AddListener(OpenSceneInCurrentPage);
            InstantiatePages();
            UpdateVisiblePage();
        }

        private void InstantiatePages()
        {
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                var pageGameObject = GameObject.Instantiate(pagePrefab, pagesContainer.transform);
                var page = pageGameObject.GetComponent<CarouselPage>();
                Assert.IsNotNull(page);
                page.SetImage(entry.sprite);
                entryInstances.Add(pageGameObject);
            }
        }

        public void GoToNextPage()
        {
            currentPage++;
            if (currentPage >= entries.Count)
            {
                currentPage = 0;
            }
            UpdateVisiblePage();
        }

        public void GoToPreviousPage()
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = entries.Count - 1;
            }
            UpdateVisiblePage();
        }

        public void OpenSceneInCurrentPage()
        {
            var sceneName = entries[currentPage].sceneName;
            SceneManager.LoadScene(sceneName);
        }

        public void UpdateVisiblePage()
        {
            for (int i = 0; i < entryInstances.Count; i++)
            {
                entryInstances[i].SetActive(currentPage == i);
            }
        }
    }
}
