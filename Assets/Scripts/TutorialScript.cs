using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject currentInstruction;

    public KeyCode nextKey = KeyCode.Return;

    public enum InstructionState
    {
        Move,
        Slider,
        HungerSlider,
        TempSlider,
        Hail,
        Campfire,
        Oxygen,
        TorchSlider,
        TorchToggle,
        TorchSwing,
        MazeEnd
    }

    public InstructionState currentState = InstructionState.Move;

    [Header("UI Elements")]
    public GameObject moveInstructions;
    public GameObject sliderInstructions;
    public GameObject hungerSliderInstructions;
    public GameObject tempSliderInstructions;
    public GameObject hailInstructions;
    public GameObject campfireInstructions;
    public GameObject oxygenInstructions;
    public GameObject torchSliderInstructions;
    public GameObject torchToggleInstructions;
    public GameObject torchSwingInstructions;
    public GameObject mazeEndInstructions;

    // Start is called before the first frame update
    void Start()
    {
        HideAllInstructions();
        currentInstruction = moveInstructions;
        ShowCurrentInstruction();
    }

    void Update()
    {
        if(Input.GetKeyDown(nextKey))
        {
            MoveToNextInstruction();
        }
    }

    private void MoveToNextInstruction()
    {
        HideCurrentInstruction();

        currentState = (InstructionState)(((int)currentState + 1) % System.Enum.GetValues(typeof(InstructionState)).Length);

        ShowCurrentInstruction();
    }

    public void ShowCurrentInstruction() {
        // Update temperature based on the current state
        switch (currentState)
        {
            // Showing move instruction
            case InstructionState.Move:
                currentInstruction = moveInstructions;
                break;

            // Showing slider instruction
            case InstructionState.Slider:
                currentInstruction = sliderInstructions;
                break;

            // Showing hunger slider instruction
            case InstructionState.HungerSlider:
                currentInstruction = hungerSliderInstructions;
                currentInstruction.SetActive(true);
                break;

            // Showing temp slider instruction
            case InstructionState.TempSlider:
                currentInstruction = tempSliderInstructions;
                break;

            // Showing hail instruction
            case InstructionState.Hail:
                currentInstruction = hailInstructions;
                break;

            // Showing campfire instruction
            case InstructionState.Campfire:
                currentInstruction = campfireInstructions;
                break;

            // Showing oxygen instruction
            case InstructionState.Oxygen:
                currentInstruction = oxygenInstructions;
                break;

            // Showing torch slider instruction
            case InstructionState.TorchSlider:
                currentInstruction = torchSliderInstructions;
                break;

            // Showing torch toggle instruction
            case InstructionState.TorchToggle:
                currentInstruction = torchToggleInstructions;
                break;

            // Showing torch swing instruction
            case InstructionState.TorchSwing:
                currentInstruction = torchSwingInstructions;
                break;

            // Showing maze end instruction
            case InstructionState.MazeEnd:
                currentInstruction = mazeEndInstructions;
                break;
        }

        if(currentInstruction != null) 
        {
            currentInstruction.SetActive(true);
        }

    }

    private void HideCurrentInstruction()
    {
        if(currentInstruction != null)
        {
            currentInstruction.SetActive(false);
        }
    }

    private void HideAllInstructions()
    {
        moveInstructions.SetActive(false);
        sliderInstructions.SetActive(false);
        hungerSliderInstructions.SetActive(false);
        tempSliderInstructions.SetActive(false);
        hailInstructions.SetActive(false);
        campfireInstructions.SetActive(false);
        oxygenInstructions.SetActive(false);
        torchSliderInstructions.SetActive(false);
        torchToggleInstructions.SetActive(false);
        torchSwingInstructions.SetActive(false);
        mazeEndInstructions.SetActive(false);
    }

    public void SetSliderState()
    {
        HideCurrentInstruction();
        currentState = InstructionState.Slider;
        currentInstruction = sliderInstructions;
        ShowCurrentInstruction();
    }
}
