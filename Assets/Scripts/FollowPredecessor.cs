using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPredecessor : MonoBehaviour
{
    public FollowPredecessor predecessor;
    public FollowPredecessor successor;

    public Vector3 oldPos;
    public LinkedListNode<Vector3> location;

    void Start()
    {
        if (predecessor)
        { predecessor.successor = this; }
    }

    public void Move()
    {
        location = location.Previous;
        transform.position = location.Value;
        if (successor)
        {
            successor.Move();
        } else {
            location.List.RemoveLast();
        }
    }

    public void ExtendPath(Vector3 direction) {
        float moveSpeed = FindObjectOfType<PlayerMovement>().moveSpeed;
        for (float i=0; i<=1; i+=moveSpeed) {
            location.List.AddLast(transform.position + direction * i);
        }
    }

    public void AddTail(FollowPredecessor newTailBit)
    { 
        if(successor)
        { 
            successor.AddTail(newTailBit);
            return;
        }

        Vector3 dir = Vector3.Normalize(location.Value - location.Previous.Value);
        ExtendPath(dir);

        successor = newTailBit;
        successor.location = location.List.Last;
        successor.transform.position = successor.location.Value;
        successor.predecessor = this;
        successor.SetColor(GetComponent<Renderer>().material.color);
    }

    public void SetColor(Color c) {
        GetComponent<Renderer>().material.color = c;

        if (successor) {
            successor.SetColor(c);
        }
    }
}
