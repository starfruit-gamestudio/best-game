using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower :MonoBehaviour
{

    Vector3[] pathNode;
    GameObject target;
    float MoveSpeed;
    float Timer;
    Vector3 CurrentPositionHolder;
    int CurrentNode;
    private Vector2 startPosition;


    // Use this for initialization
    public void StartRun(GameObject _target, Vector3[] _path, float _speed)
    {
        //pathNode = GetComponentInChildren<>();
        target = _target;
        pathNode = _path;
        MoveSpeed = _speed;
        CurrentNode = 0;
        CheckNode();
    }

    void CheckNode()
    {
        Timer = 0;
        startPosition = target.transform.position;
        CurrentPositionHolder = pathNode[CurrentNode];
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (target.transform.position != pathNode[pathNode.Length - 1])
            {
                Timer += Time.deltaTime * MoveSpeed;

                if (target.transform.position != CurrentPositionHolder)
                {

                    target.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
                }
                else
                {

                    if (CurrentNode < pathNode.Length - 1)
                    {
                        CurrentNode++;
                        CheckNode();
                    }
                }
            }
            else
                target = null;
        }
    }
}