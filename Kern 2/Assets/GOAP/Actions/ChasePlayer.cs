using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : GAction
{
    public GameObject player;

    public override bool PrePerform()
    {
        target = player;
        agent.speed += 2f;
        GWorld.Instance.GetWorld().ModifyState("chasingPlayer", 1);
        return true;
    }

    public void Update()
    {
        if (running)
        {
            target = player;
            if(Vector3.Distance(this.transform.position, player.transform.position) <= 5f)
            {
                GWorld.Instance.GetWorld().ModifyState("closeToPlayer", 1);
            }
        }
    }

    public override bool PostPerfom()
    {
        target = null;
        agent.speed -= 2f;
        return true;
    }

}
