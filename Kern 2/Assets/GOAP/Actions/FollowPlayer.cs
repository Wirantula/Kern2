using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : GAction
{
    public GameObject player;

    public override bool PrePerform()
    {
        target = player; 
        return true;
    }

    public void LateUpdate()
    {
        if (running)
        {
            target = player;
            if(agent.remainingDistance <= 0.5f)
            {
                running = false;
            }
        }
    }

    public override bool PostPerfom()
    {
        target = null;
        return true;
    }

}
