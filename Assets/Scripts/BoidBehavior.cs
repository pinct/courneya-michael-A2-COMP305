using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    BoidController controller;
    [SerializeField] private float hitDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<BoidController>();
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePosition;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector3(mousePosition.x, mousePosition.y, 0f);
    }
    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        List<BoidBehavior> members = controller.GetNeighbors(this, controller.separationRadius);
        if (members.Count == 0)
        {
            return separateVector;
        }
        foreach (BoidBehavior member in members)
        {
            Vector3 movingTowards = transform.position - member.transform.position;
            if (movingTowards.magnitude > 0)
            {
                separateVector += movingTowards.normalized / movingTowards.magnitude;
            }
        }

        return separateVector.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        if (Physics2D.Raycast(transform.position, Vector3.forward, hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, Vector3.forward, hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, Vector3.up, hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, Vector3.up, hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, Vector3.down, hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, Vector3.down, hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, Vector3.left, hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, Vector3.left, hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, Vector3.right, hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, Vector3.right, hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, new Vector3(-.5f, -.5f, 0), hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, new Vector3(-.5f, -.5f, 0), hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, new Vector3(.5f, -.5f, 0), hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, new Vector3(.5f, -.5f, 0), hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, new Vector3(-.5f, .5f, 0), hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, new Vector3(-.5f, .5f, 0), hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else if (Physics2D.Raycast(transform.position, new Vector3(.5f, .5f, 0), hitDistance))
        {
            hit = Physics2D.Raycast(transform.position, new Vector3(.5f, .5f, 0), hitDistance);
            if (hit.collider.tag != "zombie")
            {
                transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, -0.1f);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 0.1f);
        }
        //transform.position += Vector3.MoveTowards(transform.position, Separation(), 0.01f);
    }
}
