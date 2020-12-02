using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    public float cohesionRadius;
    public float cohesionPriority;

    public float alignmentRadius;
    public float alignmentPriority;

    public float separationRadius;
    public float separationPriority;

    public float avoidanceRadius;
    public float avoidancePriority;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
