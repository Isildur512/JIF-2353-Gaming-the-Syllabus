using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityPageUIManager : Singleton<AbilityPageUIManager>
{

    [SerializeField] private GameObject abilityPage;
    [SerializeField] private GameObject abilityButtonPrefab;

    [SerializeField] private GameObject abilitiesGameObject;



    private void Awake()
    {
        InitializeSingleton();
    }
    // Start is called before the first frame update

    public static void SetupAbilitiesUI()
    {
        foreach (PlayerAbility ability in CombatManager.player.abilities)
        {
            GameObject abilityButton = Instantiate(_instance.abilityButtonPrefab, _instance.abilitiesGameObject.transform);
            abilityButton.name = ability.AbilityName;

            Button button = abilityButton.GetComponent<Button>();
            button.onClick.AddListener(() => {
                AbilityController.UseAttackAbility(ability);
                TooltipManager.HideToolTip();
                CombatUIManager.ToggleAbilityPageActive();
            });

            abilityButton.GetComponent<Tooltip>().setMessage(ability.AbilityDesc);
            abilityButton.GetComponentInChildren<TMP_Text>().text = ability.AbilityName;
        }

        _instance.abilityPage.SetActive(false);
    }

    public static void AppendAbilityToPage(PlayerAbility ability)
    {
        GameObject abilityButton = Instantiate(_instance.abilityButtonPrefab, _instance.abilitiesGameObject.transform);
        abilityButton.name = ability.AbilityName;

        abilityButton.GetComponent<Tooltip>().setMessage(ability.AbilityDesc);
        abilityButton.GetComponentInChildren<TMP_Text>().text = ability.AbilityName;


        Button button = abilityButton.GetComponent<Button>();        
        button.onClick.AddListener(() => {AbilityController.UseAttackAbility(ability);
                                            TooltipManager.HideToolTip();
                                            _instance.abilityPage.SetActive(false);});
    }

}
