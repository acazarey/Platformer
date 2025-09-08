using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private float speed = 0.15f;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            transform.position = new Vector3(
                player.position.x,
                transform.position.y,
                transform.position.z
            );
        }
        
    }
}
