using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : GAgent
{
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("followPlayer", 1, false);
        SubGoal s2 = new SubGoal("defendPlayer", 1, false);
        goals.Add(s1, 2);
        goals.Add(s2, 3);
    }

}
