using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 posicaoOriginal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicaoOriginal = transform.position;
    }

    public void Drag(){
       // GameObject.Find("Image").transform.position = Mouse.current.position.ReadValue();
        print("Arrastando" + gameObject.name);
        gameObject.transform.position = Mouse.current.position.ReadValue();
    }
    
    public void moveBack()
    {
        transform.position = posicaoOriginal;
    }

    public void snap(GameObject img, GameObject lm){
        img.transform.position = lm.transform.position;
    }

    public void posicaoInicialPartes()
    {
        posicaoOriginal = transform.position;
    }

    public void checkMatch(){
        //GameObject lm1 = GameObject.Find("LM1");
        //GameObject img = GameObject.Find("Image");
        GameObject img = gameObject;
        string tag = gameObject.tag;
        GameObject lm1 = GameObject.Find("LM"+tag);
        
        float distance = Vector3.Distance(lm1.transform.position, img.transform.position);
        print("Distancia"+distance);

        if (distance <= 50)
            snap(img,lm1);
        else
            moveBack();
        
    }
    public void Drop(){
       checkMatch();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
