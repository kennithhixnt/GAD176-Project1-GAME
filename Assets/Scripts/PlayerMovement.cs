using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(up))
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if  (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        
        
        
    }
}
