using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour {

    public Answer answer;
    private const int numberOfQuestions = 10;
    
    // Start is called before the first frame update
    void Start() {

        StartCoroutine(GetRequest($"https://opentdb.com/api.php?amount={numberOfQuestions}"));
        
    }


    IEnumerator GetRequest(string uri) {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {

            yield return webRequest.SendWebRequest();

            switch (webRequest.result) {
                case UnityWebRequest.Result.Success:
                    ExtractJson(webRequest.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Error de conexión");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("Error procesando datos");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("Error de protocolo");
                    break;
            }

        }

    }

    private void ExtractJson(string jsonText) {
        
        answer = JsonUtility.FromJson<Answer>(jsonText);
        if (answer.results.Count > 0) {
            PrintResults();
        } else {
            Debug.Log("No se ha recibido ninguna pregunta.");
        }
    }

    private void PrintResults() {
        Question question = answer.results[0];
        Debug.Log($"Nº de preguntas solicitadas: {numberOfQuestions}");
        Debug.Log($"Categoría: {question.category}.");
        Debug.Log($"Dificultad: {question.difficulty}.");
        Debug.Log($"Pregunta: {question.question}.");
        Debug.Log($"Respuesta correcta: {question.correct_answer}");
    }

}
