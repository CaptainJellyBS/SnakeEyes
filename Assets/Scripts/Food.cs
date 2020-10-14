using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Vector3 destination;
    public float speed;
    public Color c;
    bool correctFood;

    private void Start()
    {
        destination = new Vector3(Random.Range(-18, 18), 0.5f, Random.Range(-8, 8));
        var m = GetComponent<Renderer>().material;
        
        m.color = c;
        m.SetColor("_EmissionColor", Color.black);
    }

    private void Update()
    {
        if(Vector3.Distance(destination, transform.position) < 0.5f)
        {
            destination = new Vector3(Random.Range(-18, 18), 0.5f, Random.Range(-8, 8));
        }
        Vector3 dir = (destination - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void GetHit()
    {
        StartCoroutine(GetHitC());
    }
    IEnumerator GetHitC()
    { 
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        transform.position = new Vector3(Random.Range(-18, 18), 0.5f, Random.Range(-8, 8));

        yield return null;
        //c = Random.ColorHSV();
        c = GameController.Instance.GetRandomColor();
        GetComponent<Renderer>().material.color = c;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;

    }
}
