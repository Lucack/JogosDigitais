using UnityEngine;
using System.Collections.Generic;


public class Geramundo : MonoBehaviour
{
    public GameObject[] plataformas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject falsoPlayer = new GameObject("FalsoPlayer");
        for (int i = 0; i < 30; i++) {
            int numeroPlataforma = Random.Range(0, plataformas.Length);
            GameObject p = Instantiate(
            plataformas[numeroPlataforma],
            falsoPlayer.transform.position,
            falsoPlayer.transform.rotation);

            // acetar escada que sobe
            if ( plataformas[numeroPlataforma].tag == "StairsUp")
            {
                falsoPlayer.transform.Translate(0, 5, 0);
            }
            else if ( plataformas[numeroPlataforma].tag == "StairsDown")
            {
                falsoPlayer.transform.Translate(0, -5, 0);
                p.transform.Rotate(new Vector3(0, 180, 0));
                p.transform.position = falsoPlayer.transform.position;
            }
            else if(plataformas[numeroPlataforma].tag == "PlataformaT"){
                if (Random.Range(0,2) == 0){
                    falsoPlayer.transform.Rotate(new Vector3(0, 90, 0));
                }
                else{
                    falsoPlayer.transform.Rotate(new Vector3(0, -90, 0));
                }
                falsoPlayer.transform.Translate(Vector3.forward * -10);
            }
            falsoPlayer.transform.Translate(Vector3.forward * -10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
