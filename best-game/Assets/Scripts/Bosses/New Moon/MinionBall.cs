using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBall : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 velocity;
    Rigidbody2D rb;
    [SerializeField]GameObject minion;
    public void Fire(float speed, Vector3 euler)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = euler;
        velocity = transform.right * speed;
        Debug.Log(velocity);
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(minion,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
