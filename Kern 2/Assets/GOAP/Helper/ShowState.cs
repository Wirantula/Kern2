using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowState : MonoBehaviour
{
    public Text display;
    public GAgent agent;

    public void Start()
    {
        agent = this.GetComponent<GAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = agent.currentAction.ToString();
    }
}
