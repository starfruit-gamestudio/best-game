using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class Operation 
{
    [SerializeField] string action;
    [SerializeField] int count;
    [SerializeField] float delay;

    public string GetAction()
    {
        return action;
    }
    public int GetCount()
    {
        return count;
    }
    public float GetDelay()
    {
        return delay;
    }
}
