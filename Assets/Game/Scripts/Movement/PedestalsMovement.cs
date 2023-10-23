using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PedestalsMovement : MonoBehaviour
{
    public List<Transform> pedestals = new List<Transform>();
    private Transform nextPedestal;
    public Transform cameraTransform;
    private int currentPedestal = -1;

    public ScenarioConfig config;
    void Start()
    {
        Move();
    }

    void Update()
    {
        if (config.isMoving)
        {
            transform.Translate(Vector3.Normalize(config.moveDirection) * config.speed * Time.deltaTime);
        }
        if (Mathf.Abs(cameraTransform.position.z - nextPedestal.position.z) < config.stopDistance)
        {
            Pause();
        }
    }

    private bool NextPedestal()
    {
        if (currentPedestal < pedestals.Count - 1)
        {
            currentPedestal++;
            nextPedestal = pedestals[currentPedestal];
            return true;
        }
        return false;
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
}
