using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class RebindingScript : MonoBehaviour
{
    [SerializeField] private InputActionReference InputActionReference;

    [SerializeField] private bool excludeMouse = true;
    [Range(0, 10)]
    [SerializeField] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] 
    private InputBinding inputBinding;
    private int bindingIndex;

    private string actionName;

    [Header("UI fields")]
    [SerializeField] private Text actionText;
    [SerializeField] private Button rebindButton;
    [SerializeField] private Text rebindText;

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());

        if(InputActionReference != null)
        {
            GetBindingInfo();
            UpdateUI();
        }
    }

    private void OnValidate()
    {
        if (InputActionReference != null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if(InputActionReference.action != null)
            actionName = InputActionReference.action.name;

        if(InputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = InputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    private void UpdateUI()
    {
        if (actionText != null)
            actionText = rebindText;

        if(rebindText != null)
        {
            if (Application.isPlaying)
            {

            }
            else
                rebindText.text = InputActionReference.action.GetBindingDisplayString(bindingIndex);
        }
    }

    private void DoRebind()
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText);
    }
}
