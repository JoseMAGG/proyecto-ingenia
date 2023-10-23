using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Questions : ScriptableObject
{
    [SerializeField] private List<string> questionList;

    public List<string> QuestionList {  get { return questionList; } }
}
