using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public void GetHit()
    {
        StartCoroutine(GetHitC());
    }
    IEnumerator GetHitC()
    {
        Collider c = GetComponent<Collider>();
        c.enabled = false;
        transform.position = new Vector3(Random.Range(-18, 18), 0.5f, Random.Range(-8, 8));

        yield return new WaitForSeconds(0.5f);
        c.enabled = true;

    }
}
