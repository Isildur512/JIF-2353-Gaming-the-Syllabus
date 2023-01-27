using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : CombatUnit
{
    public class Actions {
        public static Action basicAttack {get;} = new Action(0, "basicAttack");
        public static Action rest {get;} = new Action(1, "rest");

        public Actions() {}
    }
}
