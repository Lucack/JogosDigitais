using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ManageMosaicoGame : MonoBehaviour
{
    public Image parte;
    public Image localMarcado;
    public TextMeshProUGUI timeText;
    public AudioClip winClip;
    public AudioClip errorClip;
    public AudioClip finalAudio;

    public string menuSceneName = "MenuPrincipal";

    float lmLargura, lmAltura;

    float timer;
    float gameTimer;
    bool partesEmbaralhadas = false;
    bool gameFinished = false;

    public AudioClip inicioAudio;

    public static ManageMosaicoGame instance;

    void Awake()
    {
        instance = this;
    }

    void criarLocaisMarcados()
    {
        lmLargura = 100;
        lmAltura = 100;

        float numLinhas = 5;
        float numColunas = 5;

        float linha, coluna;

        for (int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoDireito").transform.position;

            linha = i % 5;
            coluna = i / 5;

            Vector3 lmPosicao = new Vector3(
                posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
                posicaoCentro.z
            );

            Image lm = (Image)Instantiate(localMarcado, lmPosicao, Quaternion.identity);
            lm.tag = "" + (i + 1);
            lm.name = "LM" + (i + 1);
            lm.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    public void criarPartes()
    {
        lmLargura = 100;
        lmAltura = 100;

        float numLinhas, numColunas;
        numLinhas = numColunas = 5;

        float linha, coluna;

        for (int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;

            linha = i % 5;
            coluna = i / 5;

            Vector3 lmPosicao = new Vector3(
                posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
                posicaoCentro.z
            );

            Image lm = (Image)Instantiate(parte, lmPosicao, Quaternion.identity);
            lm.tag = "" + (i + 1);
            lm.name = "Parte" + (i + 1);
            lm.transform.SetParent(GameObject.Find("Canvas").transform);

            Sprite[] todasSprites = Resources.LoadAll<Sprite>("cubone");
            Sprite s1 = todasSprites[i];
            lm.GetComponent<Image>().sprite = s1;
        }
    }

    void embaralhaPartes()
{
    int[] novoArray = new int[25];

    for (int i = 0; i < 25; i++)
    {
        novoArray[i] = i;
    }

    int tmp;

    for (int t = 0; t < 25; t++)
    {
        tmp = novoArray[t];
        int r = Random.Range(t, 25);
        novoArray[t] = novoArray[r];
        novoArray[r] = tmp;
    }

    float linha, coluna, numLinhas, numColunas;
    numLinhas = numColunas = 5;

    for (int i = 0; i < 25; i++)
    {
        linha = (novoArray[i]) % 5;
        coluna = (novoArray[i]) / 5;

        Vector3 posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;
        var g = GameObject.Find("Parte" + (i + 1));

        Vector3 novaposicao = new Vector3(
            posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
            posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
            posicaoCentro.z
        );

        g.transform.position = novaposicao;
        g.GetComponent<DragAndDrop>().posicaoInicialPartes();
    }
}
    void falaPlay()
    {
        GameObject.Find("totemPlay").GetComponent<tocadorPlay>().playPlay();
    }

    public void CheckWin()
    {
        if (gameFinished) return;

        bool allCorrect = true;
        for (int i = 1; i <= 25; i++)
        {
            GameObject parteObj = GameObject.Find("Parte" + i);
            GameObject lmObj = GameObject.Find("LM" + i);

            if (parteObj != null && lmObj != null)
            {
                if (Vector3.Distance(parteObj.transform.position, lmObj.transform.position) > 0.1f)
                {
                    allCorrect = false;
                    break;
                }
            }
            else
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            gameFinished = true;
            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        print("Mosaico completo! Iniciando sequência final.");
        
        if (finalAudio != null)
        {
            tocadorInicio.instance.PlayAudio(finalAudio, 1f);
            yield return new WaitForSeconds(finalAudio.length);
        }
        else
        {
            // Fallback case if finalAudio is not assigned
            yield return new WaitForSeconds(1f);
        }

        print("Retornando ao menu...");
        SceneManager.LoadScene(menuSceneName);
    }


    void Start()
    {
        criarLocaisMarcados();
        criarPartes();
        tocadorInicio.instance.PlayAudio(inicioAudio, 1f);
        //embaralhaPartes();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >=4 && !partesEmbaralhadas)
        {
            embaralhaPartes();
            falaPlay();
            partesEmbaralhadas = true;
        }

        if (partesEmbaralhadas && !gameFinished)
        {
            gameTimer += Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = "Tempo: " + gameTimer.ToString("F1") + "s";
            }
        }
    }

}
