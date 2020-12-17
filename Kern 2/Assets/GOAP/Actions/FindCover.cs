using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCover : GAction
{
    public GameObject[] coveredAreas;
    public float closest;
    public GameObject currentCover;

    public override bool PrePerform()
    {
        foreach(GameObject g in coveredAreas)
        {
            if(closest <=0f)
            {
                closest = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
                currentCover = g;
            }
            else if(Vector3.Distance(this.gameObject.transform.position, g.transform.position) < closest)
            {
                closest = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
                currentCover = g;
            }
        }
        target = currentCover;
        return true;
    }

    public void LateUpdate()
    {
        
    }

    public override bool PostPerfom()
    {
        target = null;
        return true;
    }

}
