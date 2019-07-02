using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    [SerializeField] List<Operation> operations;
    public Operation[] GetOperation()
    {
        return operations.ToArray();
    }
}
    
