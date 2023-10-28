using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Answers : ScriptableObject
{
    [SerializeField] private List<bool> answerList;
    public List<bool> AnswersList { get { return answerList; } }
}
