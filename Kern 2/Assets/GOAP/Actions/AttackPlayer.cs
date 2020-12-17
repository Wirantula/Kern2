using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : GAction
{
    public GameObject player;
    public bool hasAttacked = false;

    public override bool PrePerform()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 5f)
        {
            target = player;
            agent.speed += 2f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        if (running)
        {
            if (!GWorld.Instance.GetWorld().HasState("playerInSight"))
            {
                running = false;
            }
            if (Vector3.Distance(this.transform.position, player.transform.position) < 1.5f)
            {
                if (!hasAttacked)
                {
                    hasAttacked = true;
                    GWorld.Instance.GetWorld().RemoveState("closeToPlayer");
                    player.GetComponent<Player>().lives--;
                    target = null;
                    running = false;
                }
            }
        }
    }

    public override bool PostPerfom()
    {
        hasAttacked = false;
        target = null;
        agent.speed -= 2f;
        return true;
    }

}
