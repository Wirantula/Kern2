using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Grenade());
    }

    public IEnumerator Grenade()
    {
        yield return new WaitForSeconds(15f);
        this.gameObject.SetActive(false);
    }
}
