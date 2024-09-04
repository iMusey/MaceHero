using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D bigColl;

    public Vector3 prevPos = Vector3.zero;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.Distance(transform.position, prevPos) / Time.deltaTime;
        prevPos = transform.position;
    }
}
