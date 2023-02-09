using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour {

    public Answer answer;
    private const int numberOfQuestions = 10;
    private bool isPosibleToGame = false;
    private int index;
    private int i,j;
    private List<string> answers;
    //private string questionDisplay ="";

    private Question quest;
    private string correctAnswerDisplay;

    private bool waitingAnswer = false;
    private int correctAnswer;
    
    // Start is called before the first frame update
    void Start() {

        StartCoroutine(GetRequest($"https://opentdb.com/api.php?amount={numberOfQuestions}"));
        index=-1;
        Invoke("NextQuestion",0.1f);

    }

    void Update() {

        if (! isPosibleToGame || index==numberOfQuestions) {
            return;
        }


        if (! waitingAnswer) {

            answers = new List<string>();

            quest = answer.results[index];
            //questionDisplay = quest.question;
            correctAnswer = Random.Range(0,quest.incorrect_answers.Length);
            waitingAnswer = true;            

            j=0;

            for (i = 0; i<=quest.incorrect_answers.Length; i++) {
                if (i == correctAnswer) {
                    answers.Add(quest.correct_answer);
                } else {
                    answers.Add(quest.incorrect_answers[j]);
                    j++;
                }
            }

        } else {

            string userInput = Input.inputString.ToUpper();

            if (userInput != "") {
                if ((userInput[0] - '0') == correctAnswer) {
                    correctAnswerDisplay = "<<<<< Respuesta acertada>>>>>: " + answers[correctAnswer];                    
                } else {
                    correctAnswerDisplay = "<<<<< Respuesta errónea >>>>> La respuesta correcta es: " + answers[correctAnswer];
                }
                Invoke("NextQuestion", 3);
            }
        }

    }

    private void NextQuestion() {
        waitingAnswer = false;
        index++;
        correctAnswerDisplay ="";
    }

    private void OnGUI() {
        if (isPosibleToGame) {
            GUI.Label(new Rect(10, 40, 800, 30), quest.question);
            for (int i=0; i<answers.Count; i++) {
                GUI.Label(new Rect(30, 70 + i * 30, 800, 30), i + " -> " + answers[i]);
            }

            GUI.Label(new Rect(10, 200, 800, 30), "Respuesta ... ");
            GUI.Label(new Rect (10, 250, 800, 30), correctAnswerDisplay);
        }
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
            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<< " + answer.results.Count);
            isPosibleToGame = true;
        } else {
            Debug.Log("No se ha recibido ninguna pregunta.");
        }
    }

}
