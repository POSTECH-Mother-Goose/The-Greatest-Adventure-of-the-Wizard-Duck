using PlayerInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
    , IBeginDragHandler
    , IDragHandler
    , IEndDragHandler

{
    public bool isDragging = false;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        if (delta.x == 0.0f || delta.y == 0.0f)
        {
            inputs.LookInput(new Vector2(0, 0));
            isDragging = false;
            return;
        }
        delta.y *= -1;
        delta *= 3000 / Screen.width;
        Debug.Log(delta);
        inputs.LookInput(delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Scoring.increaseDrag();
        Debug.Log("end drag");
        inputs.LookInput(new Vector2(0, 0));
        isDragging = false;
    }
}
