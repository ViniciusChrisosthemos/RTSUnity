using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private GameObject m_root;
    [SerializeField] private Slider m_sliderHealthBar;

    private HealthController m_controller;
    private Transform m_target;


    private void Start()
    {
        if (CameraManager.Instance != null)
        {
            m_target = CameraManager.Instance.Camera.transform;
        }
    }

    private void Update()
    {
        /*
        if (m_target != null)
        {
            transform.LookAt(m_target.position);
        }
        */

        transform.rotation = Quaternion.identity;
    }

    public void Setup(HealthController healthController)
    {
        m_controller = healthController;

        m_sliderHealthBar.maxValue = healthController.MaxHealth;
        m_sliderHealthBar.value = healthController.CurrentHealth;

        m_controller.OnHealthChanded += HandleHealthChanded;

        m_root.SetActive(true);
    }

    private void HandleHealthChanded(float currentHealth, float valueChanged)
    {
        m_sliderHealthBar.value = currentHealth;

        if (!m_controller.IsAlive())
        {
            m_root.SetActive(false);
        }
    }
}
