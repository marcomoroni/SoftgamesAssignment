using System.Collections.Generic;
using UnityEngine;

namespace SoftgamesAssignment.MagicWords
{
    [System.Serializable]
    public class DialogueData
    {
        public readonly List<DialogueEntry> entries;
        public readonly Dictionary<string, Emoji> emojies;
        // --- it's not ideal, but here we use display names as the keys because the data is received like that
        public readonly Dictionary<string, Avatar> avatars;

        public DialogueData(List<DialogueEntry> entries, Dictionary<string, Emoji> emojies, Dictionary<string, Avatar> avatars) {
            this.entries = entries;
            this.emojies = emojies;
            this.avatars = avatars;
        }
    }

    [System.Serializable]
    public class DialogueEntry
    {
        public readonly string name;
        public readonly string text;

        public DialogueEntry(string name, string text)
        {
            this.name= name;
            this.text= text;
        }
    }

    [System.Serializable]
    public class Emoji
    {
        public readonly Texture2D image; 

        public Emoji(Texture2D image)
        {
            this.image= image;
        }
    }

    [System.Serializable]
    public class Avatar {
        public readonly Texture2D image; 
        public readonly AvatarPosition position;

        public Avatar(Texture2D image, AvatarPosition position)
        {
            this.image = image;
            this.position = position;
        }
    }

    [System.Serializable]
    public enum AvatarPosition { Left, Right }
}
