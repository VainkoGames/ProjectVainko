using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    private void Start()
    {
        
    }

   
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
            this.gameObject.GetComponent<Animator>().SetBool("RUN", true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("FRONTFLIP");
            }
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.GetComponent<Animator>().SetBool("RUN", false);
        }
    }
}
