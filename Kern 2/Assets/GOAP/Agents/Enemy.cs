using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GAgent
{
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("defendSite", 1, false);
        SubGoal s2 = new SubGoal("attackPlayer", 1, false);
        goals.Add(s1, 1);
        goals.Add(s2, 2);
    }

}
