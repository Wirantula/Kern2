using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutPatrolPoint : GAction
{
    public GameObject player;
    public override bool PrePerform()
    {
        target = this.gameObject;
        return true;
    }

    public void Update()
    {
        if (running)
        {
            transform.Rotate(Vector3.up, 360 * Time.deltaTime * 0.5f);
            Vector3 targetDir = player.transform.position - this.transform.position;
            float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
            Debug.DrawRay(this.transform.position, targetDir, Color.red, 2f);
            if (angleToPlayer >= -60 && angleToPlayer <= 60)
            {
                RaycastHit hit;
                if(Physics.Raycast(this.transform.position, targetDir, out hit))
                {
                    Debug.Log("" + hit.transform.name);
                    if(hit.transform.name == "Player")
                    {
                        Debug.Log("i saw the player");
                        GWorld.Instance.GetWorld().ModifyState("playerInSight", 1);
                    }
                }
                running = false;
            }
        }

    }

    public override bool PostPerfom()
    {
        return true;
    }

}
