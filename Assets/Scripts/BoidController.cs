using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public Transform memberPrefab;
    public int numberOfMembers;
    public List<BoidBehavior> members;
    public float spawnRadius;
    public float separationRadius;
    // Start is called before the first frame update
    void Start()
    {
        members = new List<BoidBehavior>();

        Spawn(memberPrefab, numberOfMembers);

        members.AddRange(FindObjectsOfType<BoidBehavior>());
    }

    void Spawn(Transform prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(i, i%3, 0), Quaternion.identity);
        }
    }
    public List<BoidBehavior> GetNeighbors(BoidBehavior member, float radius)
    {
        List<BoidBehavior> neightborsFound = new List<BoidBehavior>();

        foreach (BoidBehavior otherMember in members)
        {
            if (otherMember == member)
            {
                continue;
            }

            if (Vector3.Distance(member.transform.position, otherMember.transform.position) <= radius)
            {
                neightborsFound.Add(otherMember);
            }
        }

        return neightborsFound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
