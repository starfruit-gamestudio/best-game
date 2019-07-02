using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NewMoon : Boss
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void ShadowWall()
    {
        StartCoroutine(SpawnShadowWall());
    }

    public void MinionBall()
    {
        StartCoroutine(SpawnMinionBall());
    }

    IEnumerator SpawnShadowWall()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("WALL");
        busy = false;
    }
    IEnumerator SpawnMinionBall()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("BALL");
        busy = false;
    }

}
