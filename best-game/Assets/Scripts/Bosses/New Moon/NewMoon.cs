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
    [SerializeField] float wallSpawnDelay;
    [SerializeField] float wallLifeTime;
    [SerializeField] float wallDamage;
    [SerializeField] float sideWallSpawnDelay;
    [SerializeField] float sideWallLifeTime;
    [SerializeField] float sideWallDamage;

    [SerializeField] float speed;
    [SerializeField] float travelTime;
    [Header("Ball Config")]
    [SerializeField] float ballSpeed;
    [SerializeField] float ballSpawnDelay;
    [SerializeField] int ballDamage;
    [SerializeField] float angleBall;
    [SerializeField] GameObject ballPrefab;
    System.Random random;
    PathFollower pathFollower;
    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
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
        if (GetCurrentStage() == 0)
        {
            Stack<Vector3> wallStack = new Stack<Vector3>();
            List<Vector2> finalPos = new List<Vector2>();
            foreach (Vector2 pos in wallFinalPositions)
                finalPos.Add(pos);
            finalPos.Remove(transform.position);
            wallStack.Push(finalPos[random.Next(finalPos.Count)]);
            wallStack = GeneratePath(wallStack);
            StartCoroutine(SpawnShadowWall(wallStack,false));
        }
        else if (GetCurrentStage() == 1)
        {
            Stack<Vector3> wallStack = new Stack<Vector3>();
            List<Vector2> finalPos = new List<Vector2>();
            foreach (Vector2 pos in wallFinalPositions)
                finalPos.Add(pos);
            finalPos.Remove(transform.position);
            Vector3 final = finalPos[random.Next(finalPos.Count)];
            wallStack.Push(final);
            wallStack = GeneratePath(wallStack);
            StartCoroutine(SpawnShadowWall(wallStack, false));
            SpawnSideWall(wallStack.ToArray()[1]);

        }
        else if (GetCurrentStage() == 2)
        {

        }
        else if (GetCurrentStage() == 3)
        {

        }
    }
    void SpawnSideWall(Vector3 first)
    {
        if (first.y == transform.position.y)
        {
            Stack<Vector3> wallStack = new Stack<Vector3>();
            wallStack.Push(transform.position + new Vector3(0, 2, 0));
            wallStack.Push(transform.position + new Vector3(0, 1, 0));
            StartCoroutine(SpawnShadowWall(wallStack, true));
            wallStack = new Stack<Vector3>();
            wallStack.Push(transform.position - new Vector3(0, 2, 0));
            wallStack.Push(transform.position - new Vector3(0, 1, 0));
            StartCoroutine(SpawnShadowWall(wallStack, true));
        }
        else
        {
            Stack<Vector3> wallStack = new Stack<Vector3>();
            wallStack.Push(transform.position + new Vector3(2, 0, 0));
            wallStack.Push(transform.position + new Vector3(1, 0, 0));
            StartCoroutine(SpawnShadowWall(wallStack, true));
            wallStack = new Stack<Vector3>();
            wallStack.Push(transform.position - new Vector3(2, 0, 0));
            wallStack.Push(transform.position - new Vector3(1, 0, 0));
            StartCoroutine(SpawnShadowWall(wallStack, true));
        }
    }

    Stack<Vector3> GeneratePath(Stack<Vector3> wallStack)
    {
        
        
        int distance = Mathf.FloorToInt(Vector3.Distance(new Vector3(transform.position.x, wallStack.Peek().y), wallStack.Peek())) - 1;
        int times = Mathf.FloorToInt(distance / 4);
        int _x = 1;
        if (wallStack.Peek().x > transform.position.x)
            _x = -1;

        for (int i = 0; i < times; i++)
        {
            wallStack.Push(wallStack.Peek() + new Vector3(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector3(_x, 0));
            int change = random.Next(2);
            if (change == 0)
                change = -1;
            wallStack.Push(wallStack.Peek() + new Vector3(0, change));
            wallStack.Push(wallStack.Peek() + new Vector3(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector3(_x, 0));
            wallStack.Push(wallStack.Peek() + new Vector3(0, change * -1));
        }
        while (wallStack.Peek().x != transform.position.x)
        {
            wallStack.Push(wallStack.Peek() + new Vector3(_x, 0));
        }

        distance = Mathf.FloorToInt(Vector3.Distance(transform.position, wallStack.Peek())) - 1;
        times = Mathf.FloorToInt(distance / 4);

        int _y = 1;
        if (wallStack.Peek().y > transform.position.y)
            _y = -1;
        for (int i = 0; i < times; i++)
        {
            wallStack.Push(wallStack.Peek() + new Vector3(0, _y));
            wallStack.Push(wallStack.Peek() + new Vector3(0, _y));
            int change = random.Next(2);
            if (change == 0)
                change = -1;
            wallStack.Push(wallStack.Peek() + new Vector3(change, 0));
            wallStack.Push(wallStack.Peek() + new Vector3(0, _y));
            wallStack.Push(wallStack.Peek() + new Vector3(0, _y));
            wallStack.Push(wallStack.Peek() + new Vector3(change * -1, 0));
        }
        while (wallStack.Peek().y != transform.position.y)
        {
            wallStack.Push(wallStack.Peek() + new Vector3(0, _y));
        }
        return wallStack;
    }

    IEnumerator SpawnShadowWall(Stack<Vector3>wallStack, bool side,bool clone = false)
    {
        Vector3[] path = wallStack.ToArray();
        while (wallStack.Count>0)
        {
            if (side)
            {
                yield return new WaitForSeconds(sideWallSpawnDelay);
                ShadowWall sw = Instantiate(WallPrefab, wallStack.Pop(), Quaternion.identity).GetComponent<ShadowWall>();
                sw.DelaySpawn(sideWallSpawnDelay, sideWallLifeTime);
            }
            else
            {
                yield return new WaitForSeconds(wallSpawnDelay);
                ShadowWall sw = Instantiate(WallPrefab, wallStack.Pop(), Quaternion.identity).GetComponent<ShadowWall>();
                sw.DelaySpawn(wallSpawnDelay, wallLifeTime);
            }
        }
        if (clone)
        {

        }
        if (!side)
        {
            pathFollower.StartRun(gameObject, path, speed);
            yield return new WaitForSeconds(travelTime);
            busy = false;
        }
        
    }

    //Minion
    public void MinionBall()
    {
        Vector3 aim = transform.GetChild(0).eulerAngles;
        if (GetCurrentStage() == 0)
        {
            StartCoroutine(SpawnMinionBall(aim));
        }
        else if (GetCurrentStage() == 1)
        {
            Debug.Log("Estagio 2");
            StartCoroutine(SpawnMinionBall(aim));
            StartCoroutine(SpawnMinionBall(aim + new Vector3(0, 0, angleBall)));
            StartCoroutine(SpawnMinionBall(aim + new Vector3(0, 0, -angleBall)));
        }
    }
    
    IEnumerator SpawnMinionBall(Vector3 euler)
    {
        yield return new WaitForSeconds(ballSpawnDelay);
        Debug.Log("Spawnou");
        MinionBall _ball = Instantiate(ballPrefab, transform.position, Quaternion.identity).GetComponent<MinionBall>();
        _ball.Fire(ballSpeed,euler);
        busy = false;
    }


}
