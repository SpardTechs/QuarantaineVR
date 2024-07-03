using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveDroneWithInput : MonoBehaviour
{
    public List<DebugInputActionReference_Float> m_floatInput = new List<DebugInputActionReference_Float>();
    public List<DebugInputActionReference_Vector2> m_vector2Input = new List<DebugInputActionReference_Vector2>();

    [System.Serializable]
    public class DebugInputActionReference_Float
    {

        public string m_description = "Reminder...";
        public InputActionReference m_whatToObserve;
        public float m_value;
        public UnityEvent<float> m_onChanged;

        public void StartListening()
        {

            m_whatToObserve.action.Enable();
            m_whatToObserve.action.performed += (e) => {
                m_value = e.ReadValue<float>();

                m_onChanged.Invoke(m_value);
            };
            m_whatToObserve.action.canceled += (e) => {
                m_value = e.ReadValue<float>();

                m_onChanged.Invoke(m_value);
            };
        }

        public void StopListening()
        {

            m_whatToObserve.action.Disable();
            m_whatToObserve.action.performed -= (e) => {
                m_value = e.ReadValue<float>();

                m_onChanged.Invoke(m_value);
            };
            m_whatToObserve.action.canceled -= (e) => {
                m_value = e.ReadValue<float>();

                m_onChanged.Invoke(m_value);
            };
        }
    }
    [System.Serializable]
    public class DebugInputActionReference_Vector2
    {
        public string m_description = "Reminder...";
        public InputActionReference m_whatToObserve;
        public Vector2 m_value;
        public UnityEvent<Vector2> m_onChanged;

        public void StartListening()
        {

            m_whatToObserve.action.Enable();
            m_whatToObserve.action.performed += (e) => { m_value = e.ReadValue<Vector2>(); };
            m_whatToObserve.action.canceled += (e) => {
                m_value = e.ReadValue<Vector2>();

                m_onChanged.Invoke(m_value);
            };
        }

        public void StopListening()
        {

            m_whatToObserve.action.Disable();
            m_whatToObserve.action.performed -= (e) => { m_value = e.ReadValue<Vector2>(); };
            m_whatToObserve.action.canceled -= (e) => {
                m_value = e.ReadValue<Vector2>();

                m_onChanged.Invoke(m_value);
            };
        }
    }

    void Start()
    {
        foreach (var action in m_floatInput)

            action.StartListening();
        foreach (var action in m_vector2Input)

            action.StartListening();

    }
    private void OnDestroy()
    {
        foreach (var action in m_floatInput)
            action.StopListening();
        foreach (var action in m_vector2Input)
            action.StopListening();
    }
}