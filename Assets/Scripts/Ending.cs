using PlayerInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private Animator anim;
    public PlayerObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("here");
        anim.SetTrigger("isEnd");
        player.setEnd();

        Destroy(GetComponentInChildren<Rigidbody>());
    }
}
