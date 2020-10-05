using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Snake"))
        {
            if(collision.collider.gameObject == PlayerMovement.Instance.successor.gameObject) { return; } //Don't collide with the second tail part, to avoid insta dying. Kinda ugly, but works well enough for now.
            Debug.Log("You ded");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        PlayerMovement.Instance.moveTime = 900; //Ugly Shit
        yield return new WaitForSeconds(1.0f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
 }

