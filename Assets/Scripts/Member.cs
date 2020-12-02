using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Level level;
    public MemberConfig conf;

    private Vector3 wanderTarget;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        conf = FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    protected Vector3 Wander()
    {
        float jitter = conf.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= conf.wanderRadius;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(0, conf.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;
    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        List<Member> neighbors = level.GetNeighbors(this, conf.cohesionRadius);
        if (neighbors.Count == 0)
        {
            return cohesionVector;
        }
        foreach (Member member in neighbors)
        {
            if (isInFOV(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }
        }
        if (countMembers == 0)
        {
            return cohesionVector;
        }
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.cohesionPriority * Cohesion() + conf.alignmentPriority * Alignment() + conf.separationPriority * Separation();
        return finalVec;
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        List<Member> members = level.GetNeighbors(this, conf.alignmentRadius);
        //if (members.Count == 0)
        //{
        //    return alignVector;
        //}
        foreach (Member member in members)
        {
            if (isInFOV(member.position))
            {
                alignVector += member.velocity;
            }
        }
        //return alignVector.normalized;
        return Vector3.RotateTowards(transform.position, GetMousePosition(), 10f, 10f);
    }

    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        List<Member> members = level.GetNeighbors(this, conf.separationRadius);
        if (members.Count == 0)
        {
            return separateVector;
        }
        foreach (Member member in members)
        {
            if (isInFOV(member.position))
            {
                Vector3 movingTowards = this.position - member.position;
                if (movingTowards.magnitude > 0)
                {
                    separateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }

        return separateVector.normalized;
    }

    void WrapAround(ref Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);

    }

    float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
        {
            value = min;
        }
        else if (value < min)
        {
            value = max;
        }
        return value;
    }

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    bool isInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= conf.maxFOV;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        position = position + velocity * Time.deltaTime;
        WrapAround(ref position, -level.bounds, level.bounds);
        transform.position = position;

        //acceleration = new Vector3();

        //transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), .1f);
        //List<Member> members = level.GetNeighbors(this, conf.separationRadius);
        //foreach (Member member in members)
        //{
        //    if (isInFOV(member.position))
        //    {
        //        Vector3 movingTowards = this.position - member.position;
        //        if (movingTowards.magnitude > 0)
        //        {
        //            transform.position += (Vector3)Vector2.MoveTowards(transform.position, member.position, -0.1f);
        //        }
        //    }
        //}
    }
}
