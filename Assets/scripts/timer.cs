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

    public bool fim;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            jogoRodando = !jogoRodando;
            fim = true;
        }

        if (jogoRodando){
            tempo += Time.deltaTime;
            timerVisualizador.text = tempo.ToString("0.00") + "s";

        } else {

        }
        if (fim && !jogoRodando) {
            Pontuador();
        }
    }
    void Pontuador()
    {
        fim = false;
        Jogador.Tempo = tempo;
        Jogador.Nome = inputPlayerName.GetComponent<InputField>().text;
        ScrollViewer.AtualizaPlacar();
    }
}
