using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class ChamaAbertura : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AbrirGame()
    {
        SceneManager.LoadScene("Abertura");
    }

}
