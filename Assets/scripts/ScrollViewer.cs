using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewer : MonoBehaviour
{
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;

    static RectTransform prefabPrivate;
    static ScrollRect scrollViewPrivate;
    static RectTransform contentPrivate;
    static Scores scores;

    static List<ExampleItemView> views = new List<ExampleItemView>();
    static string filePath = "assets/armazenamentoDados/placar.json";

    // Start is called before the first frame update
    void Start()
    {
        prefabPrivate = prefab;
        scrollViewPrivate = scrollView;
        contentPrivate = content;

        string json = "";

        using (StreamReader r = new StreamReader(filePath))
        {
            json = r.ReadToEnd();
        }

        Scores result = JsonUtility.FromJson<Scores>(json);
        scores = result;

        OnReceivedNewModels(result.scores);

    }


    void OnReceivedNewModels(ScoreItem[] models)
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        views.Clear();
        foreach (ScoreItem model in models)
        {
            GameObject instance = GameObject.Instantiate(prefabPrivate.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            ExampleItemView view = InitializeItemView(instance, model);

            views.Add(view);
        }
    }

    public static void AtualizaPlacar()
    {
        foreach (Transform child in contentPrivate)
            Destroy(child.gameObject);

        views.Clear();
        Debug.Log(Jogador.Nome);
        Debug.Log(Jogador.Tempo);

        ScoreItem model = new ScoreItem();

        model.Nome = Jogador.Nome;
        model.Tempo = Jogador.Tempo;


        GameObject instance = GameObject.Instantiate(prefabPrivate.gameObject) as GameObject;
        instance.transform.SetParent(contentPrivate.GetComponent<Transform>().transform);

        bool menorTempo = false;
        ScoreItem Score= null;

        views.Clear();
        ScoreItem[] resultados = new ScoreItem[scores.scores.Length];
        for(int i=0 ;i< scores.scores.Length;i++)
            resultados[i] = scores.scores[i];
        resultados[scores.scores.Length - 1] = model;

        ScoreItem repos = null;
        for (int i = 0; i < scores.scores.Length; i++) {
            for (int j = 0; j < scores.scores.Length - (i + 1); j++) {
                if (resultados[j].Tempo > resultados[j + 1].Tempo)
                {
                    repos = resultados[j];
                    resultados[j] = resultados[j + 1];
                    resultados[j + 1] = repos;
                }
            }
        }

        scores.scores = resultados;
        string jsonString = JsonUtility.ToJson(scores);

        using (StreamWriter streamWriter = File.CreateText(filePath))
        {
            streamWriter.Write(jsonString);
        }

        foreach (ScoreItem score in resultados) {
            instance = GameObject.Instantiate(prefabPrivate.gameObject) as GameObject;
            instance.transform.SetParent(contentPrivate, false);
            ExampleItemView view = InitializeItemView(instance, score);

            views.Add(view);
        }

    }
    static ExampleItemView InitializeItemView(GameObject viewGameObject, ScoreItem model)
    {
        ExampleItemView view = new ExampleItemView(viewGameObject.transform);

        view.nome.text = model.Nome;
        view.pontos.text = model.Tempo.ToString();

        return view;
    }

    public class ExampleItemView
    {

        public Text nome;
        public Text pontos;
        public ExampleItemView(Transform rootView)
        {
            nome = rootView.Find("Nome").GetComponent<Text>();
            pontos = rootView.Find("Pontos").GetComponent<Text>();
        }
    }

    [Serializable]
    public class ScoreItem
    {
        public string Nome;
        public float Tempo;
    }

    [Serializable]
    public class Scores
    {
        public ScoreItem[] scores;
    }

}
