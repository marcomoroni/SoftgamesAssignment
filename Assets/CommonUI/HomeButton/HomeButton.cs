using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftgamesAssignment.CommonUI
{
    [AddComponentMenu("Softgames Assignment/Common UI/Home Button"), RequireComponent(typeof(Button)), DisallowMultipleComponent]
    public class HomeButton : MonoBehaviour
    {
        private const string homeSceneName = "Home";

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(GoToHomeScene);
        }

        private void GoToHomeScene()
        {
            SceneManager.LoadScene(homeSceneName);
        }
    }
}
