using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class RicochetGun : MonoBehaviour
{
    [SerializeField] GameObject flareLight;
    Rigidbody2D rb;
    BoxCollider2D bC2D;
    [SerializeField] float consumeRate;
    [SerializeField] float consumeDelay;
    Light2D light2D;
    bool startConsuming;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bC2D = GetComponent<BoxCollider2D>();
        light2D = flareLight.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Consume();

        if (rb.velocity.magnitude < 1)
        {
            bC2D.enabled = false;
        }
    }

    void Consume()
    {
        if (!startConsuming)
        {
            consumeDelay -= Time.deltaTime;
        }

        if (consumeDelay <= 0)
        {
            startConsuming = true;
        }

        if (startConsuming)
        {
            light2D.intensity -= Time.deltaTime * consumeRate;
        }

        if (light2D.intensity <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}

