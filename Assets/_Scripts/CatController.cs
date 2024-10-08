using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpSpeed;
    bool IsGrounded;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 newPos = new Vector2(
                gameObject.transform.position.x + speed * Time.deltaTime,
                gameObject.transform.position.y
                );
            gameObject.transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 newPos = new Vector2(
                gameObject.transform.position.x - speed * Time.deltaTime,
                gameObject.transform.position.y
                );
            gameObject.transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(IsGrounded){
            Vector2 newPos = new Vector2(
                gameObject.transform.position.x, gameObject.transform.position.y + jumpSpeed * Time.deltaTime
            );
            gameObject.transform.position = newPos;
            }
        }
    }

    void OnTriggerStay(Collider other){
        if (other.Transform.Tag == "Ground"){
            IsGrounded = true;
            Debug.Log("Grounded");
        }
        else{
            IsGrounded = false;
            Debug.Log("Not Grounded");
        }
    }
}
