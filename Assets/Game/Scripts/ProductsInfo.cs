using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsInfo : MonoBehaviour
{
    [SerializeField] private Questions questionsInfo;
    [SerializeField] private Answers answersInfo;
    private int counter;

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
        answersInfo.AnswersList[counter - 1] = answer;
    }

    public int GetQuestionsCount() { return questionsInfo.QuestionList.Count; }
}
