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
    LogMessage
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
        _ => throw new NotImplementedException(),
    };
}
