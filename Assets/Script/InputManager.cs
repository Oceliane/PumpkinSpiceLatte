using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static PlayerInputAction inputActions;

    private void Awake()
    {
        if(inputActions == null)
            inputActions = new PlayerInputAction();
    }

    public static void StartRebind(string actionName, int bindingIndex, Text statusText)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Input Manager : couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                DoRebind(action, bindingIndex, statusText, true);

        }
        else
            DoRebind(action, bindingIndex, statusText, false);
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, Text statusText, bool allCompositeParts)
    {
        if (actionToRebind != null || bindingIndex < 0)
            return;

        statusText.text = "Press a button";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if(allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts);
            }
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
        });

        rebind.Start();
    }
}
