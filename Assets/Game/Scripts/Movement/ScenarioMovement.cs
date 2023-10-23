using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMovement : MonoBehaviour
{
    public ScenarioConfig config;
    public List<Transform> cityBackgroud;
    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= initialPos.z - config.cycleLength)
        {
            transform.position = initialPos;
        } 
        if (config.isMoving)
        {
            Vector3 translation = Vector3.Normalize(config.moveDirection) * config.speed * Time.deltaTime;
            transform.Translate(translation);
            foreach (Transform transform in cityBackgroud)
            {
                transform.Translate(translation / config.cityDistance * -1);
            }
        }
    }
}
