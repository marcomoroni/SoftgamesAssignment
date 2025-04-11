using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace SoftgamesAssignment.MagicWords
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject textBubblePrefab;
        [SerializeField] private GameObject textBubblesContainer;

        public void CreateDialogue(DialogueData data)
        {
            for (int i = 0; i < data.entries.Count; i++)
            {
                var entry = data.entries[i];
                var textBubbleGameObject = GameObject.Instantiate(textBubblePrefab, textBubblesContainer.transform);
                var textBubble = textBubbleGameObject.GetComponent<DialogueRow>();
                Assert.IsNotNull(textBubble);
                textBubble.SetText(entry.text);
                if (data.avatars.TryGetValue(entry.name, out var avatar))
                {
                    var image = avatar.image;
                    var sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f)); // --- Should not create one every time.
                    textBubble.SetImage(sprite);
                }
            }
        }
    }
}
