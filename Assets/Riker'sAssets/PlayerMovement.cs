using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
  
    GameManager gameManager;
    
    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !gameManager.activeConvo)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        } 
        else if (Input.GetKey(KeyCode.RightArrow) && !gameManager.activeConvo)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (gameManager.activeConvo && (Input.GetKey(KeyCode.RightArrow) ||Input.GetKey(KeyCode.LeftArrow)))
        {
            Debug.Log("can't move while in convo, sorry bruv");
        }
    }

    private void OnTriggerEnter2D (Collider2D c) 
    {
        Debug.Log("encountered " + c.name);
        // c.GetComponent<npc>().talkable = true;
        gameManager.activeNPC = c.name;
    }

    private void OnTriggerExit2D (Collider2D c)
    {
        Debug.Log("can't speak to " + c.name + " anymore");
        // c.GetComponent<npc>().talkable = false;
        gameManager.activeNPC = "";
    }
}
