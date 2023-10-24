using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PedestalsMovement : MonoBehaviour
{
    public List<Transform> pedestals = new List<Transform>();
    public float betweenDistance;
    public Transform cameraTransform;
    public ScenarioConfig config;
    public AnimationClip startAnimation;

    private Transform nextPedestal;
    private int currentPedestal = -1;
    private Transform lastPedestal;

    void Start()
    {
        Pause();
        PlacePedestals();
    }

    private void PlacePedestals()
    {
        for (int i = 0; i < pedestals.Count; i++)
        {
            Vector3 position = pedestals[i].position;
            position.z = betweenDistance * (i + 1);
            pedestals[i].position = position;
        }

    }

    void Update()
    {
        if (config.isMoving)
        {
            transform.Translate(Vector3.Normalize(config.moveDirection) * config.speed * Time.deltaTime);
            if (Mathf.Abs(cameraTransform.position.z - nextPedestal.position.z) < config.stopDistance)
            {
                Pause();
            }
        }
        if (lastPedestal != null)
        {
            if (cameraTransform.position.z - lastPedestal.position.z > config.stopDistance)
            {
                Vector3 position = lastPedestal.localPosition;
                position.z += betweenDistance * 2;
                lastPedestal.localPosition = position;
            }
        }
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
}
