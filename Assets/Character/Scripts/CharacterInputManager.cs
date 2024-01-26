using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActionAsset;

    private InputActionMap gameplayActionMap;

    private InputAction moveAction;

    private void Awake()
    {
        gameplayActionMap = inputActionAsset.FindActionMap("gameplay");
        moveAction = gameplayActionMap.FindAction("move");
        gameplayActionMap.FindAction("vault").performed += OnVault;
        gameplayActionMap.FindAction("dash").performed += OnDash;
    }

    private void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.magnitude > 0f)
        {
            Debug.Log("MOVE: " + moveVector);
        }
    }

    void OnEnable()
    {
        gameplayActionMap.Enable();
    }
    void OnDisable()
    {
        gameplayActionMap.Disable();
    }


    private void OnVault(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("VAULT");
    }

    private void OnDash(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("DASH");
    }
}
