using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Boid[] b;
    Rigidbody rb;
    public float f1, f2, f3;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        b = GameObject.FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoid();
    }

    void MoveBoid()
    {
        Vector3 v1, v2, v3;
        foreach (Boid boid in b)
        {
            v1 = Rule1(boid);
            v2 = Rule2(boid);
            v3 = Rule3(boid);

            rb.velocity = v1.normalized + v2.normalized + v3.normalized;
            rb.transform.Translate(rb.velocity.normalized * Time.deltaTime);
        }
    }

    Vector3 Rule1(Boid boid)
    {
        Vector3 center;
        int allBoids = 0;
        Vector3 allBoidPositions = new Vector3(0,0,0);

        foreach (Boid b in boid.b)
        {
            if (b != boid)
            {
                allBoids++;
                allBoidPositions += b.transform.position;
            }
        }
        center = (allBoidPositions - boid.transform.position / (allBoids - 1));
        return (center - boid.transform.position) / f1;
    }

    Vector3 Rule2(Boid boid)
    {
        Vector3 c = new Vector3(0,0,0);
        foreach (Boid b in boid.b)
        {
            if (b != boid)
            {
                if(Vector3.Distance(b.transform.position, boid.transform.position) < f2)
                {
                    c = c - (b.transform.position - boid.transform.position);
                }
            }
        }
        return c;
    }

    Vector3 Rule3(Boid boid)
    {
        Vector3 pv = new Vector3(0,0,0);
        int allBoids = 0;

        foreach (Boid b in boid.b)
        {
            if (b != boid)
            {
                allBoids++;
                pv += b.rb.velocity;
            }
        }
        pv = pv / allBoids;
        return (pv - boid.rb.velocity) / f3;
    }
}
