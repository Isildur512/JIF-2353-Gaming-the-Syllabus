using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    [SerializeField] private string relativePathFromSpritesFolder;

    private void Awake()
    {
        DatabaseManager.OnSpritesLoaded += SetSprite;
    }

    private void Start()
    {
        // We might have cases where the DBManager loads everything before the SpriteLoader is loaded (e.g. switching scenes)
        if (DatabaseManager.ContentIsLoaded(DatabaseManager.Loadable.Everything))
        {
            SetSprite();
        }
    }

    private void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Path.Combine(Files.SpritesFolderRelative, relativePathFromSpritesFolder));
    }
}
