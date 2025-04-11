using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoftgamesAssignment.Home
{
    public class CarouselPage : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}