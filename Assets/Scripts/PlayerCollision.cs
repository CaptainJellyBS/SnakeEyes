using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Snake"))
        {
            if (collision.collider.gameObject == PlayerMovement.Instance.successor.gameObject) { return; } //Don't collide with the second tail part, to avoid insta dying. Kinda ugly, but works well enough for now.
            Debug.Log("You ded");
            StartCoroutine(Die());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameController.Instance.score += 100;
            PlayerMovement.Instance.AddTail();
            other.enabled = false; //Unity decided that sometimes we should collide with food twice before it gets destroyed. I disagree. Fuck you Unity.
            Destroy(other.gameObject);
        }
    }

    IEnumerator Die()
    {
        PlayerMovement.Instance.moveTime = 900; //Ugly Shit
        yield return new WaitForSeconds(1.0f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
 }

