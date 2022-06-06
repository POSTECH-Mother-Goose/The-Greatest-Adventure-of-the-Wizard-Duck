using PlayerInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private Animator anim;
    private bool soundPlayed;

    public AudioSource soundWin;
    public AudioSource soundLose;

    public PlayerObject player;
    public Scoring scoring;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoring.isGameOver && !soundLose.isPlaying)
        {
            if (!soundPlayed)
            {
                soundLose.Play();
                soundPlayed = true;
            }
            transform.position = Vector3.MoveTowards(
                transform.position, 
                transform.position-new Vector3(transform.position.x, -0xffffffff, transform.position.z), 
                Time.deltaTime
            );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!scoring.isGameOver)
        {
            if (!soundPlayed)
            {
                soundWin.Play();
                soundPlayed = true;
            }
            anim.SetTrigger("isEnd");
            player.setEnd();

            Destroy(GetComponentInChildren<Rigidbody>());

            scoring.Ending();
        }
    }
}
