using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Config")]
    [SerializeField] protected int life;
    [SerializeField] protected int actualLife;
    [SerializeField] protected int damage;
    [SerializeField] protected List<Stage> stages;
    [SerializeField]List<Operation> actionQueue;
    bool alive;
    bool abort;
    protected bool busy;
    int runningStage;
    int indexAction;
    protected Type child;

    // Start is called before the first frame update
    protected void Start()
    {
        actualLife = life;
        GetActionQueue();
    }
    protected void Update()
    {
        if (runningStage != GetCurrentStage())
        {
            abort = true;
            GetActionQueue();
            indexAction = 0;
        }
        else if(!busy)
        {
            busy = true;
            if(abort)
                abort = false;
            StartCoroutine (ExecuteMethod(actionQueue[indexAction]));
            indexAction++;
            if (indexAction == actionQueue.Count)
            {
                indexAction = 0;
            }

        }
    }
    protected void GetActionQueue()
    {
        runningStage = GetCurrentStage();
        actionQueue = new List<Operation>();
        foreach (Operation op in stages[runningStage].GetOperation())
        {
            for (int i = 0; i < op.GetCount(); i++)
            {
                actionQueue.Add(op);
            }
        }
    }

    protected IEnumerator ExecuteMethod(Operation method)
    {
        yield return new WaitForSeconds(method.GetDelay());
        MethodInfo methodInfo = child.GetMethod(method.GetAction());
        string result = (string)methodInfo.Invoke(this, null);
    }

    protected int GetCurrentStage()
    {
        float _stageSize = life/stages.Count;
        int _stage = Mathf.CeilToInt(actualLife / _stageSize);
        _stage = Mathf.Clamp(stages.Count - _stage, 0, stages.Count - 1);
        return _stage;
    }
}
