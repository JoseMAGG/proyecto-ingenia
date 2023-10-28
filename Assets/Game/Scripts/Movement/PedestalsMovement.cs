using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PedestalsMovement : MonoBehaviour
{
    public List<Pedestal> pedestals = new List<Pedestal>();
    public float betweenDistance;
    public Transform cameraTransform;
    public ScenarioConfig config;
    public AnimationClip startAnimation;
    public AnimationClip endAnimation;
    public Animator animator;

    private Pedestal nextPedestal;
    private int currentPedestal = -1;
    private Pedestal lastPedestal;
    private int pedestalCount = 0;
    private bool isEnd;
    private bool isMoving;

    #region Singleton
    private static PedestalsMovement instance;

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

    public static PedestalsMovement GetInstance()
    {
        return instance;
    }
    #endregion

    void Start()
    {
        config.isMoving = false;
        PlacePedestals();
    }

    private void PlacePedestals()
    {
        for (int i = 0; i < pedestals.Count; i++)
        {
            Vector3 position = pedestals[i].transform.position;
            position.z = betweenDistance * (i + 1);
            pedestals[i].transform.position = position;
        }

    }

    void Update()
    {
        if (config.isMoving && !isEnd)
        {
            transform.Translate(Vector3.Normalize(config.moveDirection) * config.speed * Time.deltaTime);
            if (Mathf.Abs(cameraTransform.position.z - nextPedestal.transform.position.z) < config.stopDistance)
            {
                Pause();
            }
        }
        if (lastPedestal != null)
        {
            CheckMovePedestal();
        }
    }

    private void CheckMovePedestal()
    {
        if (cameraTransform.position.z - lastPedestal.transform.position.z > config.stopDistance
                        && pedestalCount < ProductsInfo.GetInstance().GetQuestionsCount())
        {
            if (!isMoving) StartCoroutine(MovePedestal());
        }
    }

    private IEnumerator MovePedestal()
    {
        isMoving = true;
        lastPedestal.ResetAnimation();
        Vector3 position = lastPedestal.transform.localPosition;
        yield return new WaitForSeconds(0.5f);
        position.z += betweenDistance * 2;
        lastPedestal.transform.localPosition = position;
        isMoving = false;
    }

    private bool NextPedestal()
    {
        if (pedestals.Count == 0) return false;
        if (currentPedestal > -1) lastPedestal = pedestals[currentPedestal];
        currentPedestal++;
        if (currentPedestal >= pedestals.Count)
        {
            currentPedestal = 0;
        }
        nextPedestal = pedestals[currentPedestal];
        return true;
    }
    public void Move()
    {
        config.isMoving = true;
        NextPedestal();
    }
    private void Pause()
    {
        config.isMoving = false;
        pedestalCount++;
        string text;
        Sprite icon;
        bool isNext = ProductsInfo.GetInstance().SetNextQuestion(out text, out icon);
        if (isNext)
        {
            nextPedestal.quesiton.text = text;
            nextPedestal.icon.sprite = icon;
            nextPedestal.ShowInfo();
        }
        else
        {
            StartCoroutine(End());
        }

    }
    public void StartPressed()
    {
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(startAnimation.length);
        Move();
    }

    private IEnumerator End()
    {
        isEnd = true;
        foreach (Pedestal pedestal in pedestals)
        {
            pedestal.gameObject.SetActive(false);
        }
        config.isMoving = true;
        animator.SetTrigger("End");
        yield return new WaitForSeconds(endAnimation.length);
        config.isMoving = false;
        SceneManager.LoadScene(1);
    }

    internal void CatchAnswer(bool isYes)
    {
        ProductsInfo.GetInstance().SaveAnswer(isYes);
        Move();
    }
}
