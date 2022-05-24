using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
    , IDragHandler
    , IEndDragHandler

{
    private StarterAssetsInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        inputs = player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        delta.y *= -1;
        delta /= 50;
        Debug.Log(delta);
        inputs.LookInput(delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        inputs.LookInput(new Vector2(0, 0));
    }
}
