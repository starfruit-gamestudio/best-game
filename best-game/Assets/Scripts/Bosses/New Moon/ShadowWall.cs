using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWall : MonoBehaviour
{   
    Animator anim;
    Collider2D collider;
    private void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }
    public void DelaySpawn(float spawn, float destroy)
    {
        StartCoroutine(Spawn(spawn,destroy));
    }
    IEnumerator Spawn(float spawn, float destroy)
    {
        yield return new WaitForSeconds(spawn);
        anim.SetTrigger("Spawn");
        collider.enabled = true;
        Destroy(gameObject, destroy);
    }

}
