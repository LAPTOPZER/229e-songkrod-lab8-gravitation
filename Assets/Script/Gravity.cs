using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.0674f;
    public static List<Gravity> gravityObjectsList;
    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 10000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityObjectsList == null )
        {
            gravityObjectsList = new List<Gravity>();
        }

        gravityObjectsList.Add( this );
        if (!planet)
        {
            rb.AddForce(Vector3.back * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach ( var planet in gravityObjectsList)
        {
            if (planet != this)
            {
                Attract(planet);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance,2));
        Vector3 force = forceMagnitude * direction.normalized;

        otherRb.AddForce(force);

    }
}
