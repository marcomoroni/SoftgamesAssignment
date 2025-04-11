using System.Collections.Generic;
using UnityEngine;

// You can't set spriteCharacterTable at runtime. As a solution, create a sprite in editor that can be written, then
// use this helpers to set the parts of the texture.
// Limitation: you can obly have as many parts as you create via ediotr-
public static class TMPSpriteAssetHelper
{
    public static void ApplySprites(Texture2D mainTexture, List<Texture2D> textureForSprites) {
        mainTexture.SetPixels32(64, 64, 64, 64, textureForSprites[0].GetPixels32());
    }
}
