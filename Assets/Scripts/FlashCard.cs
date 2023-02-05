using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCard {
    public string question, answerA, answerB, answerC;
    public int indexOfResult;

    public FlashCard(
        string Question, string AnswerA, 
        string AnswerB, string AnswerC, 
        int IndexOfResult
    ) {
        question = Question;
        answerA = AnswerA;
        answerB = AnswerB;
        answerC = AnswerC;
        indexOfResult = IndexOfResult;
    }
}
