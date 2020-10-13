using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tobii.Gaming;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public float moveTime;
    public float deadZone;
    public bool useTobii;
    public static PlayerMovement Instance { get; private set; }
    public FollowPredecessor successor;
    public GameObject tailPrefab;

    private void Awake()
    {
        Instance = this;

        try {
            useTobii = TobiiAPI.IsConnected;
        } catch {
            useTobii = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
        NewColor();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddTail();
        }
    }

    Vector2 GetScreenPosition()
    {
        if (useTobii) {
            return TobiiAPI.GetGazePoint().Screen;
        } else {
            return Input.mousePosition;
        } 
    }

    Vector3 CalculateDirection(Vector3 prevDir)
    {
        Vector2 screenpoint = GetScreenPosition();
        Ray r = Camera.main.ScreenPointToRay(screenpoint);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            if (hit.collider.CompareTag("Ground")) 
            {
                if(Vector3.Distance(hit.point, transform.position) < deadZone) { return prevDir; }
                Vector3 direction = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
                return direction;
            }
        }
        return prevDir;
    }

    IEnumerator Move()
    {
        Vector3 direction = Vector3.zero;
        
        while(true)
        {
            direction = CalculateDirection(direction);
            Vector3 oldPos = transform.position;
            transform.position += direction * moveSpeed;
            transform.rotation = Quaternion.LookRotation(direction);
            successor.Move(oldPos);
            yield return new WaitForSeconds(moveTime);
        }        
    }

    public void AddTail()
    {
        GameObject newTail = Instantiate(tailPrefab);
        successor.AddTail(newTail.GetComponent<FollowPredecessor>());
    }

    public void NewColor()
    {
        // Build list of available colors
        HashSet<Color> colors = new HashSet<Color>();
        foreach (var food in FindObjectsOfType<Food>())
        {
            var col = food.GetComponent<Renderer>().material.color;
            colors.Add(col);
        }

        // Remove current color, to make sure the color always changes
        Color current = GetComponent<Renderer>().material.color;
        colors.Remove(current);

        // Check if color change is available
        if (colors.Count == 0) {
            return;
        }

        // Change to a random color
        Color newColor = colors.ElementAt(Random.Range(0, colors.Count));
        GetComponent<Renderer>().material.color = newColor;
        successor.SetColor(newColor);
    }
}
