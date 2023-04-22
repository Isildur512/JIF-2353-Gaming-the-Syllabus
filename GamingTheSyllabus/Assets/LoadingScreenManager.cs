using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingPanel;

    private void Awake()
    {
        DatabaseManager.OnAllLoadingCompleted += HidePanel;
    }

    private void HidePanel()
    {
        StartCoroutine(IPanelFadeOut());
    }

    private IEnumerator IPanelFadeOut()
    {
        while (loadingPanel.alpha > 0)
        {
            loadingPanel.alpha -= Time.deltaTime * 0.5f;
            yield return null;
        }
        loadingPanel.gameObject.SetActive(false);
    }
}
