using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvalidSyllabusCodeAlertManager : Singleton<InvalidSyllabusCodeAlertManager>
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void ShowAlert()
    {
        _instance.StartCoroutine(IShowAlert());
    }

    private static IEnumerator IShowAlert()
    {
        _instance.text.alpha = 1;

        yield return new WaitForSeconds(2.0f);

        while (_instance.text.alpha > 0)
        {
            _instance.text.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
