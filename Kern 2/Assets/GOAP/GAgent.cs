using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach(GAction a in acts)
        {
            actions.Add(a);
        }
    }


    //laat action completed worden
    private bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerfom();
        invoked = false;
    }

    void LateUpdate()
    {
        if(currentAction != null && currentAction.running)
        {
            //check voor de afstand van de agent tot de actie locatie
            if(currentAction.agent.hasPath && currentAction.agent.remainingDistance < 0.5f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        //maak planner aan voor agent en vul met goals
        if(planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, null);
                if(actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        //zorg dat agent nooit vast loopt in actionqueue
        if(actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if(currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                if(currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }

    }
}
