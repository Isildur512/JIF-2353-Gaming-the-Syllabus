using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// I made this class for Actions instead of enums. It basically acts like enums though. Make sure you add actions in the order they are presented in xml files.
public class Action {
    public string Name {get; private set;}
        public int Value {get; private set;}
        public Action(int val, string name) {
            Value = val;
            Name = name;
    }
}
