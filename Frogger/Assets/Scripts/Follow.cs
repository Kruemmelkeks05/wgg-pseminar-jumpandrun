using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y = (player.transform.position + new Vector3 (0, 2.5f, -10)).y;
        transform.position = position;
    }
}
