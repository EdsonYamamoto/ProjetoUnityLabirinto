using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerVisualizador;
    public float tempo;
    public bool jogoRodando;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            jogoRodando = !jogoRodando;
        }

        if (jogoRodando){

            tempo += Time.deltaTime;
            timerVisualizador.text = tempo.ToString("0.00") + "s";

        }
    }
}
