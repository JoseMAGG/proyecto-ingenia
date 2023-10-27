using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsInfo : MonoBehaviour
{
    public Questions questionsInfo;
    private int counter;
    private bool[] answers;

    private static ProductsInfo instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        answers = new bool[questionsInfo.QuestionList.Count];
    }

    public static ProductsInfo GetInstance()
    {
        return instance;
    }

    public bool SetNextQuestion(out string quesiton, out Sprite icon)
    {
        if (counter < questionsInfo.QuestionList.Count)
        {
            quesiton = questionsInfo.QuestionList[counter];
            icon = questionsInfo.QuestionImages[counter];
            counter++;
            return true;
        }
        quesiton = null;
        icon = null;
        return false;
    }

    public void SaveAnswer(bool answer)
    {
        answers[counter - 1] = answer;
    }
    public bool[] GetAnswers() { return answers; }

    public int GetQuestionsCount() { return questionsInfo.QuestionList.Count; }
}
