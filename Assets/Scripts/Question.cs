using System.Collections;
using System.Collections.Generic;

public class Question  {

    private string category;
    private string type;
    private string difficulty;
    private string question;
    private string correct_answer;
    private string[] incorrect_answers;

    public Question (string _category,
                     string _type,
                     string _difficulty,
                     string _question,
                     string _correct_answer,
                     string[] _incorrect_answers) {
        
        category = _category;
        type = _type;
        difficulty = _difficulty;
        question = _question;
        correct_answer = _correct_answer;
        incorrect_answers = _incorrect_answers;

    }

    // getters de propiedades
    public string getCategory() {return this.category;}

    public string getType() {return this.type;}

    public string getDifficulty() {return this.difficulty;}

    public string getQuestion() {return this.question;}

    public string getCorrectAnswer() {return this.correct_answer;}
    
    public string[] getIncorrectAnswers() {return this.incorrect_answers;}

}