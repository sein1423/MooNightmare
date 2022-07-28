using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputVector;
    private bool isInput;
    bool SetCenter = false;

    public PlayerController player;
    
    public enum JoystichType { Move, Attack }
    public JoystichType joystickType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        ControlJoyStickLever(eventData);
    }
    
    public void ControlJoyStickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir :
            inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End");
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        switch (joystickType)
        {
            case JoystichType.Move:
                player.Move(Vector2.zero);
                break;
            case JoystichType.Attack:
                player.Attack(Vector2.zero);
                break;
        }
    }

    public void DragEnd()
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        switch (joystickType)
        {
            case JoystichType.Move:
                player.Move(Vector2.zero);
                break;
            case JoystichType.Attack:
                player.Attack(Vector2.zero);
                break;
        }
    }
    
    private void InputcontrolVecter()
    {
        switch (joystickType)
        {
            case JoystichType.Move:
                player.Move(inputVector);
                break;
            case JoystichType.Attack:
                player.Attack(inputVector);
                break;
        }
    }


    void Update()
    {

        if (isInput)
        {
            InputcontrolVecter();
        }
    }
}
