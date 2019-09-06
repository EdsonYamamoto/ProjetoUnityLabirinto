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
    // Start is called before the first frame update
    void Start()
    {
        prefabPrivate = prefab;
        scrollViewPrivate = scrollView;
        contentPrivate = content;

        string json = "";

        using (StreamReader r = new StreamReader("assets/armazenamentoDados/placar.json"))
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

        ExampleItemView view = InitializeItemView(instance, model);

        views.Clear();
        
        foreach (ScoreItem score in scores.scores)
        {
            if (model.Tempo < score.Tempo)
                Debug.Log("Verdade");
            
        }
        foreach (ScoreItem score in scores.scores)
        {
            instance = GameObject.Instantiate(prefabPrivate.gameObject) as GameObject;
            instance.transform.SetParent(contentPrivate, false);
            view = InitializeItemView(instance, score);

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
