using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : GAction
{
    public GameObject[] patrolPoints;
    public int patrolIndex = 0;
    public GameObject player;
    public GameObject view;

    public override bool PrePerform()
    {
        target = patrolPoints[patrolIndex];
        return true;
    }

    public void Update()
    {
        if (running)
        {
            transform.Rotate(Vector3.up, 180 * Time.deltaTime * 0.3f);
            Vector3 targetDir = player.transform.position - view.transform.position;
            float angleToPlayer = (Vector3.Angle(targetDir, view.transform.forward));
            if (angleToPlayer >= -60 && angleToPlayer <= 60)
            {
                RaycastHit hit;
                if (Physics.Raycast(view.transform.position, targetDir, out hit))
                {
                    if (hit.transform.name == "Player")
                    {
                        Debug.DrawRay(view.transform.position, targetDir, Color.red, 1f);
                        GWorld.Instance.GetWorld().ModifyState("playerInSight", 1);
                    }
                }
                //running = false;
            }
        }

    }

    public override bool PostPerfom()
    {
        if(patrolIndex < patrolPoints.Length -1)
        {
            patrolIndex++;
        }
        else
        {
            patrolIndex = 0;
        }
        return true;
    }

}
