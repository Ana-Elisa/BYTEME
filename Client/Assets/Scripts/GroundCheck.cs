using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerC player;

     void Start()
    {
        player = gameObject.GetComponentInParent<PlayerC>();
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        player.grounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
