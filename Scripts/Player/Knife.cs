using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float mag = 1f;
    [SerializeField] float spinSpeed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spin();
    }

    void FixedUpdate()
    {
        Spin();
    }

    void Spin()
    {
        if (rb.velocity.magnitude > mag)
        {
            transform.Rotate(0, 0, spinSpeed, Space.World);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            rb.velocity *= -0.1f;
        }
    }
}
