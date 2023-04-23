using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    [SerializeField] private string relativePathFromSpritesFolder;

    private void Awake()
    {
        // We might have cases where the DBManager loads everything before the SpriteLoader is loaded (e.g. switching scenes)
        if (DatabaseManager.ContentIsLoaded(DatabaseManager.Loadable.Sprites))
        {
            SetSprite();
        } else
        {
            DatabaseManager.OnSpritesLoaded += SetSprite;
        }
    }

    private void OnDestroy()
    {
        DatabaseManager.OnSpritesLoaded -= SetSprite;
    }

    private void SetSprite()
    {
        byte[] fileContents = File.ReadAllBytes(Path.Combine(Files.SpritesFolderAbsolute, relativePathFromSpritesFolder));

        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(fileContents);

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width,
            texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
