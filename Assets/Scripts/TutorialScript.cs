using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Game objects
    public GameObject currentInstruction;
    public KeyCode nextKey = KeyCode.Return;
    public GameObject food;
    public GameObject hungerSlider;
    public GameObject tempSlider;
    public GameObject oxygenSlider;
    public GameObject torchSlider;
    public GameObject hail;
    public GameObject campfire;
    public GameObject endZone;

    // Script references
    public HailController hailController;

    // Instruction states
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

    // Current state
    public InstructionState currentState = InstructionState.Move;

    // Text game objects
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
    public GameObject pressEnter;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the text instructions
        HideAllInstructions();

        // Hide other objects
        food.SetActive(false);
        hail.SetActive(false);
        campfire.SetActive(false);
        endZone.SetActive(false);

        // Hide the sliders
        HideSliders();

        // Set the current instruction to the first instruction
        currentInstruction = moveInstructions;

        // Show the current instruction
        ShowCurrentInstruction();
    }

    void Update()
    {
        // If the player presses the next key and the state isn't one of the instructions that requires a specific action
        if(Input.GetKeyDown(nextKey) && currentState != InstructionState.Move && currentState != InstructionState.HungerSlider && currentState != InstructionState.TorchToggle && currentState != InstructionState.TorchSwing && currentState != InstructionState.MazeEnd)
        {
            // Move to the next instruction
            MoveToNextInstruction();
        }
    }

    public void MoveToNextInstruction()
    {
        // Hide the current instruction
        HideCurrentInstruction();

        // Set the current state to the next state in the InstructionState enum
        currentState = (InstructionState)(((int)currentState + 1) % System.Enum.GetValues(typeof(InstructionState)).Length);

        // Show the new instruction
        ShowCurrentInstruction();
    }

    public void ShowCurrentInstruction() {
        // Update temperature based on the current state
        switch (currentState)
        {
            // Showing move instruction
            case InstructionState.Move:
                currentInstruction = moveInstructions;
                pressEnter.SetActive(false);
                break;

            // Showing slider instruction
            case InstructionState.Slider:
                currentInstruction = sliderInstructions;
                ShowPressEnter();
                hungerSlider.SetActive(true);
                tempSlider.SetActive(true);
                break;

            // Showing hunger slider instruction
            case InstructionState.HungerSlider:
                currentInstruction = hungerSliderInstructions;
                pressEnter.SetActive(false);
                currentInstruction.SetActive(true);
                food.SetActive(true);
                tempSlider.SetActive(false);
                break;

            // Showing temp slider instruction
            case InstructionState.TempSlider:
                tempSlider.SetActive(true);
                ShowPressEnter();
                currentInstruction = tempSliderInstructions;
                hungerSlider.SetActive(false);
                break;

            // Showing hail instruction
            case InstructionState.Hail:
                currentInstruction = hailInstructions;
                ShowPressEnter();
                tempSlider.SetActive(true);
                hail.SetActive(true);
                hailController.enabled = true;
                break;

            // Showing campfire instruction
            case InstructionState.Campfire:
                currentInstruction = campfireInstructions;
                ShowPressEnter();
                hail.SetActive(false);
                hailController.enabled = false;
                campfire.SetActive(true);
                break;

            // Showing oxygen instruction
            case InstructionState.Oxygen:
                currentInstruction = oxygenInstructions;
                ShowPressEnter();
                campfire.SetActive(false);
                tempSlider.SetActive(false);
                oxygenSlider.SetActive(true);
                break;

            // Showing torch slider instruction
            case InstructionState.TorchSlider:
                currentInstruction = torchSliderInstructions;
                ShowPressEnter();
                campfire.SetActive(true);
                oxygenSlider.SetActive(false);
                torchSlider.SetActive(true);
                break;

            // Showing torch toggle instruction
            case InstructionState.TorchToggle:
                currentInstruction = torchToggleInstructions;
                pressEnter.SetActive(false);
                break;

            // Showing torch swing instruction
            case InstructionState.TorchSwing:
                currentInstruction = torchSwingInstructions;
                pressEnter.SetActive(false);
                break;

            // Showing maze end instruction
            case InstructionState.MazeEnd:
                currentInstruction = mazeEndInstructions;
                pressEnter.SetActive(false);
                torchSlider.SetActive(false);
                campfire.SetActive(false);
                endZone.SetActive(true);
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

    private void ShowPressEnter()
    {
        pressEnter.SetActive(true);
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
        pressEnter.SetActive(false);
    }

    private void HideSliders()
    {
        hungerSlider.SetActive(false);
        tempSlider.SetActive(false);
        oxygenSlider.SetActive(false);
        torchSlider.SetActive(false);
    }
}
