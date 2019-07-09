using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBall : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity;
    Rigidbody2D rb;
    [SerializeField]GameObject minion;
    public void Fire(float speed, Vector3 target)
    {
        velocity = (transform.forward).normalized*speed;
        rb = GetComponent<Rigidbody2D>();
        transform.LookAt(target);
        
        rb.velocity = velocity;
    }
    public void Fire(float speed, Vector3 target, float euler)
    {
        Debug.Log("Firo");
        velocity = Vector3.forward * speed;
        rb = GetComponent<Rigidbody2D>();
        transform.LookAt(target);
        transform.eulerAngles += new Vector3(0,0,euler);

        rb.velocity = velocity;
    }
    public void Fire(float speed)
    {
        velocity = (transform.forward).normalized * speed;
        rb = GetComponent<Rigidbody2D>();
        transform.LookAt(Vector3.zero);
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(minion,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
