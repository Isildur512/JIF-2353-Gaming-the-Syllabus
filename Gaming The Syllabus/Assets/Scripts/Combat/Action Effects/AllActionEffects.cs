using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This needs to contain every ActionEffect that exists. When you create new ones, make sure to add them here and to AllActionEffects.
/// </summary>
public enum ActionEffects
{
    DamageTarget,
    LogMessage,
    Heal,
    RandomDamageTarget
}

/// <summary>
/// This needs to contain every ActionEffect that exists. When you create new ones, make sure to add them here and to ActionEffects.
/// </summary>
public static class AllActionEffects
{
    public static Type GetActionEffect(this ActionEffects effect) => effect switch
    {
        ActionEffects.DamageTarget => typeof(DamageTarget),
        ActionEffects.LogMessage => typeof(LogMessage),
        ActionEffects.Heal => typeof(HealTarget),
        ActionEffects.RandomDamageTarget => typeof(RandomDamageTarget),
        _ => throw new NotImplementedException(),
    };
}
