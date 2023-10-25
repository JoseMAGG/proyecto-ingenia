using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Questions : ScriptableObject
{
    [SerializeField] private List<string> questionList;
    [SerializeField] private List<Sprite> questionImages;

    public List<string> QuestionList {  get { return questionList; } }
    public List<Sprite>  QuestionImages { get { return questionImages; } }
}
