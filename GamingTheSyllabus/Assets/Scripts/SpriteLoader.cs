using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    [SerializeField] private string relativePathFromSpritesFolder;

    private void Awake()
    {
        Debug.LogError(Path.Combine(Files.SpritesFolderRelative, relativePathFromSpritesFolder));
        DatabaseManager.OnSpritesLoaded += () => { GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Path.Combine(Files.SpritesFolderRelative, relativePathFromSpritesFolder)); };
    }
}
