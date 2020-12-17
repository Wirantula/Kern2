using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<Weapon> weapons;

    static GWorld()
    {
        world = new WorldStates();
        weapons = new Queue<Weapon>();

        Weapon[] allWeapons = GameObject.FindObjectsOfType<Weapon>();
        foreach(Weapon w in allWeapons)
        {
            weapons.Enqueue(w);
        }
        if(allWeapons.Length > 0)
        {
            world.ModifyState("weaponAvailable", allWeapons.Length);
        }
    }

    private GWorld()
    {
        
    }

    public void AddWeapon(Weapon w)
    {
        weapons.Enqueue(w);
    }

    public Weapon RemoveWeapon()
    {
        if(weapons.Count == 0)
        {
            return null;
        }
        return weapons.Dequeue();
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }

}
