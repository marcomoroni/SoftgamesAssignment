# Notes

## What I didn't manage to complete

I didn't manage to complete the Magic Words exercise. The issue that took me a huge amount of time was generating emojis that could be used at runtime. Adding custom emojis to a TextMeshPro Text is pretty easy, but only if you do it in editor: create a `TMP_SpriteAsset` with the desired emojis and use `<sprite name="my_emoji">` in the text field. I struggled a lot to edit `TMP_SpriteAsset` at runtime. I even started to work on a workaround where you'd edit the pixels texture used to make the sprites of the emojis.

The code for this exercise is therefore still work-in-progess.

Had I had not found this issue, I would have also done the following:

- support for invalid web requests (as some images do not exist)
- support for fallback images for avatars and emojis
- loading screen while fetching
- better UI, including left and right speech bubbles, initial scroll position at the bottom, better responsive text bubbles relative to the contained text

## Folder structure

I've worked in some projects where folders are used only for contain the same file types, and others where the folders contain content by topic or feature. I decided to use the latter in this project, but it's just arbitrary.
