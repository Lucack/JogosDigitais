using UnityEngine;
using System.Collections;


public class ControleAstro : MonoBehaviour
{
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }
       
    }

    void StopJumping()
    {
        anim.SetBool("isJumping", false);
    }
}
