using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NewMoon : Boss
{
    [Header("Wall Config")]
    [SerializeField] GameObject WallPrefab;
    [SerializeField] List<Vector2> wallFinalPositions;
    [SerializeField] float WallSpawnDelay;
    [SerializeField] float WallLifeTime;
    [SerializeField] float WallDamage;
    Stack<Vector2> wallStack;
    System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        child = GetType();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //Wall
    public void ShadowWall()
    {
        wallStack = new Stack<Vector2>();
        wallStack.Push(wallFinalPositions[random.Next(wallFinalPositions.Count)]);
        int distance = Mathf.FloorToInt(Vector3.Distance(new Vector3(transform.position.x, wallStack.Peek().y),wallStack.Peek()))-1;
        int times = Mathf.FloorToInt(distance / 4);
        int _x = 1;
        if (wallStack.Peek().x > transform.position.x)
            _x = -1;
        
        for (int i = 0; i < times; i++)
        {
            wallStack.Push(wallStack.Peek() + new Vector2(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector2(_x, 0));
            int change = random.Next(2);
            if (change == 0)
                change = -1;
            wallStack.Push(wallStack.Peek() + new Vector2(0, change));
            wallStack.Push(wallStack.Peek() + new Vector2(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector2(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector2(0, change*-1));
        }
        while (wallStack.Peek().x != transform.position.x)
        {
            wallStack.Push(wallStack.Peek() + new Vector2(_x, 0));
        }

        distance = Mathf.FloorToInt(Vector3.Distance(transform.position, wallStack.Peek())) - 1;
        times = Mathf.FloorToInt(distance / 4);

        int _y = 1;
        if (wallStack.Peek().y > transform.position.y)
            _y = -1;
        for (int i = 0; i < times; i++)
        {
            wallStack.Push(wallStack.Peek() + new Vector2( 0, _y));
            wallStack.Push(wallStack.Peek() + new Vector2(0, _y));
            int change = random.Next(2);
            if (change == 0)
                change = -1;
            wallStack.Push(wallStack.Peek() + new Vector2(change, 0));
            wallStack.Push(wallStack.Peek() + new Vector2(0, _y));
            wallStack.Push(wallStack.Peek() + new Vector2(0, _y));
            wallStack.Push(wallStack.Peek() + new Vector2(change * -1, 0));
        }
        while (wallStack.Peek().y != transform.position.y)
        {
            wallStack.Push(wallStack.Peek() + new Vector2( 0,_y));
        }


        StartCoroutine(SpawnShadowWall());
    }

    IEnumerator SpawnShadowWall()
    {
        while (wallStack.Count>0)
        {
            yield return new WaitForSeconds(WallSpawnDelay);
            Instantiate(WallPrefab, wallStack.Pop(), Quaternion.identity);
            Debug.Log("WALL");
        }
        busy = false;
    }

    //Minion
    public void MinionBall()
    {
        StartCoroutine(SpawnMinionBall());
    }

    
    IEnumerator SpawnMinionBall()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("BALL");
        busy = false;
    }
    

}
