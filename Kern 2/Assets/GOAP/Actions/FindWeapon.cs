using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWeapon : GAction
{
    public GameObject[] weapon;
    public override bool PrePerform()
    {
        if(weapon != null)
        {
            target = weapon[Random.Range(0, weapon.Length)];
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

    public void Update()
    {

    }

    public override bool PostPerfom()
    {
        return true;
    }

}
