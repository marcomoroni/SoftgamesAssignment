using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Assertions;


namespace SoftgamesAssignment.MagicWords.Networking
{
    public static class Urls
    {
        public static string DialogueData = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";
    }

    // Raw data structures deserialized from JSON.
    namespace FromJson
    {
        [System.Serializable]
        public class DialogueData
        {
            public List<DialogueEntry> dialogue;
            public List<Emoji> emojies;
            public List<Avatar> avatars;
        }

        [System.Serializable]
        public class DialogueEntry
        {
            public string name;
            public string text; 
        }

        [System.Serializable]
        public class Emoji
        {
            public string name;
            public string url; 
        }

        [System.Serializable]
        public class Avatar
        {
            public string name;
            public string url; 
            public string position;
        }
    }

    public static class Get
    {
        public async static Task<DialogueData> DialogueData()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(Urls.DialogueData))
            {
                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    await Task.Yield();
                }
                switch (request.result)
                {
                    case UnityWebRequest.Result.Success:
                        var rawDialogueData = JsonUtility.FromJson<FromJson.DialogueData>(request.downloadHandler.text);
                        return await ConvertFromRaw(rawDialogueData);
                    default:
                        throw new Exception($"Requesttt failed: {request.error}");
                }
            }
        }

        private static async Task<DialogueData> ConvertFromRaw(FromJson.DialogueData raw)
        {
            var avatars = raw.avatars.Select(async rawAvatar => {
                var texture = await DownloadTextureAsync(rawAvatar.url);
                Assert.IsTrue(rawAvatar.position == "left" || rawAvatar.position == "right");
                var position = rawAvatar.position == "left" ? AvatarPosition.Left : AvatarPosition.Right;
                return new Tuple<string, Avatar>(rawAvatar.name, new Avatar(texture, position));
            });
            var emojis = raw.emojies.Select(async rawEmoji => {
                // --- use temp emoji that has no errors while testing
                // var texture = await DownloadTextureAsync(rawEmoji.url);
                var texture = await DownloadTextureAsync("https://api.dicebear.com/9.x/fun-emoji/png?seed=Sawyer");
                return new Tuple<string, Emoji>(rawEmoji.name, new Emoji(texture));
            });
            // --- make all tasks parallel
            var aa = await Task.WhenAll(avatars);
            var ee = await Task.WhenAll(emojis);
            var dialogueData = new DialogueData(
                raw.dialogue.Select(rawEntry => new DialogueEntry(rawEntry.name, rawEntry.text)).ToList(),
                ee.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2) ,
                aa.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2) 
            );
            return dialogueData;
        }

        private static async Task<Texture2D> DownloadTextureAsync(string url)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                await request.SendWebRequest();
                return request.result switch
                {
                    UnityWebRequest.Result.Success => DownloadHandlerTexture.GetContent(request),
                    _ => throw new Exception($"Request failed: {request.error}"),
                };
            }
        }
    }
}
