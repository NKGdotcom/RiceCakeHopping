using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトの往復運動
/// </summary>
public class ReciprocatingMotion : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform finalPoint;

    private bool isReturn;

    private Vector3 objPos;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = objPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReturn)
        {
            objPos.x += Time.deltaTime * moveSpeed;
            transform.position = objPos;
            if(objPos.x > finalPoint.position.x)
            {
                isReturn = true;
            }
        }
        else
        {
            objPos.x -= Time.deltaTime * moveSpeed;
            transform.position = objPos;
            if(objPos.x < startPoint.position.x)
            {
                isReturn = false;
            }
        }
    }
}
