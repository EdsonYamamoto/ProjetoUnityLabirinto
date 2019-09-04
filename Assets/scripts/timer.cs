using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerVisualizador;
    public float tempo;
    public bool jogoRodando;

    public float tempoAtual;
    public string nomeJogador;
    public GameObject inputPlayerName;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            jogoRodando = !jogoRodando;
        }

        if (jogoRodando){
            tempo += Time.deltaTime;
            timerVisualizador.text = tempo.ToString("0.00") + "s";
            Jogador.Tempo = tempo;
            Jogador.Nome = inputPlayerName.GetComponent<InputField>().text;
        }
        else
        {
            //Debug.Log(inputPlayerName.GetComponent<InputField>().text);
        }
    }
}
