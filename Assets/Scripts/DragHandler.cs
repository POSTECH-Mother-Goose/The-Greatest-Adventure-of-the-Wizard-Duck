using PlayerInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
    , IDragHandler
    , IEndDragHandler

{
    private PlayerObject inputs;
    public PlayerObject player;

    // Start is called before the first frame update
    void Start()
    {
        inputs = player.GetComponent<PlayerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        delta.y *= -1;
        delta /= 1;
        Debug.Log(delta);
        inputs.LookInput(delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Scoring.increaseDrag();
        Debug.Log("end drag");
        inputs.LookInput(new Vector2(0, 0));
    }
}
