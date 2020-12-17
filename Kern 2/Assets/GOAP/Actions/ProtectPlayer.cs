using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectPlayer : GAction
{
    public GameObject player;
    public Enemy enemy;
    public GameObject smokeGrenade;
    public bool threwSmoke = false;

    public override bool PrePerform()
    {
        if (smokeGrenade.activeInHierarchy)
        {
            return false;
        }
        enemy = GameObject.FindObjectOfType<Enemy>();
        target = this.gameObject; 
        return true;
    }

    public void LateUpdate()
    {
        if (running)
        {
            if (!threwSmoke)
            {
                smokeGrenade.transform.position = player.transform.position;
                smokeGrenade.SetActive(true);
                enemy.Invoke("CompleteAction", 0f);
                GWorld.Instance.GetWorld().RemoveState("chasingPlayer");
                GWorld.Instance.GetWorld().RemoveState("playerInSight");
                threwSmoke = true;
            }
            target = this.gameObject;
        }
    }

    public override bool PostPerfom()
    {
        target = null;
        threwSmoke = false;
        return true;
    }

}
