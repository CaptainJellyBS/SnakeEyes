using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPredecessor : MonoBehaviour
{
    public FollowPredecessor predecessor;
    public FollowPredecessor successor;

    public Vector3 oldPos;

    void Start()
    {
        if (predecessor)
        { predecessor.successor = this; }
    }

    public void Move(Vector3 newPos)
    {
        oldPos = transform.position;
        transform.position = newPos;
        if (successor)
        {
            successor.Move(oldPos);
        }
    }

    public void AddTail(FollowPredecessor newTailBit)
    { 
        if(successor)
        { 
            successor.AddTail(newTailBit);
            return;
        }
        successor = newTailBit;
        successor.transform.position = oldPos;
        successor.predecessor = this;
    }
}
