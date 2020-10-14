using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    public int lives = 3;

    public int foodInterval;

    public int foodRate;
    public bool selfCollision;

    private int intervalCtr;

    void Start() {

    }

    void OnGUI() {
        GUI.Label(new Rect(0,0,100,100), "Lives: " + lives.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Snake") && selfCollision)
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
            other.GetComponent<Food>().GetHit(); //Unity decided that sometimes we should collide with food twice before it gets destroyed. I disagree. Fuck you Unity.
                                                 //Destroy(other.gameObject);
                                                 //PlayerMovement.Instance.NewColor();

            if (other.GetComponent<Food>().c == GetComponent<Renderer>().material.color) {
                GameController.Instance.score += 100;
                PlayerMovement.Instance.AddTail();

                intervalCtr++;
                if (intervalCtr >= foodInterval) {
                    for (int i=0; i<foodRate; i++) {
                        Instantiate(other.gameObject).GetComponent<Food>().GetHit();
                    }
                    intervalCtr = 0;
                }
            } else {
                lives--;

                if (lives <= 0) {
                    Debug.Log("You ded");
                    StartCoroutine(Die());
                }
            }

            PlayerMovement.Instance.StartCoroutine(PlayerMovement.Instance.UglyColorFix());
        }
    }

    IEnumerator Die()
    {
        PlayerMovement.Instance.moveTime = 900; //Ugly Shit
        yield return new WaitForSeconds(1.0f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
 }

