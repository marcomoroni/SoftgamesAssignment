using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoftgamesAssignment.MagicWords
{
    public class DialogueRow : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}
