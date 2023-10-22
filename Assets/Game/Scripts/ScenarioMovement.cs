using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMovement : MonoBehaviour
{
    public List<GameObject> pedestals = new List<GameObject>();
    public Transform cameraTransform;
    public float speed;
    private GameObject nextPedestal;
    private int currentPedestal = -1;
    private bool isMoving;

    public Vector3 moveDirection;
    public float stopDistance;

    void Start()
    {
        MoveToNext();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.Normalize(moveDirection) * speed * Time.deltaTime);
        }
        if (Mathf.Abs(cameraTransform.position.z - nextPedestal.transform.position.z) < stopDistance)
        {
            Pause();
        }
    }

    public void MoveToNext()
    {
        isMoving = NextPedestal();
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

    public void Pause()
    {
        isMoving = false;
    }
}
