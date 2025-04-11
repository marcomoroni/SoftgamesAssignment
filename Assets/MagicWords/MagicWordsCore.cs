using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace SoftgamesAssignment.MagicWords
{
    public class MagicWordsCore : MonoBehaviour
    {
        // --- temp
        [SerializeField] private TMP_Text textEl;
        [SerializeField] private Material emojiMaterial;
        [SerializeField] private DialogueUI dialogueUi;

        private async void Start()
        {
            try
            {
                var dialogueData = await Networking.Get.DialogueData();
                await Awaitable.MainThreadAsync();
                Debug.Log(dialogueData.emojies.Count);



                // var spriteAsset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
                // spriteAsset.name="Hello";
                // spriteAsset.material =emojiMaterial;
                // for (int i = 0; i < dialogueData.emojies.Count; i++)
                // {
                //     Debug.Log($"adding emoji... {i}");
                //     Debug.Log(dialogueData.emojies.ElementAt(i).Key);
                //     var tex = dialogueData.emojies.ElementAt(i).Value.image;
                //     Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                //     TMP_SpriteGlyph spriteGlyph = new TMP_SpriteGlyph
                //     {
                //         index = (uint)i,
                //         sprite = sprite
                //     };
                //     TMP_SpriteCharacter spriteCharacter = new TMP_SpriteCharacter(0xE000 + (uint)i, spriteGlyph) 
                //     {
                //         name = "Sprite_" + i
                //     };
                //     spriteAsset.spriteGlyphTable.Add(spriteGlyph);
                //     Debug.Log(spriteAsset.spriteCharacterTable == null);
                //     spriteAsset.spriteCharacterTable.Add(spriteCharacter);
                // }
                // textEl.spriteAsset = spriteAsset;






                Assert.IsNotNull(dialogueUi);
                dialogueUi.CreateDialogue(dialogueData);
                
            }
            catch (Exception ex)
            {
                Debug.LogError($":::Request failed: {ex.Message}");
            }
        }



    }
}
