using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the bar UI with a pixel of 10 pixel per unit.
/// </summary>

[RequireComponent(typeof(Slider), typeof(RectTransform))]
[ExecuteAlways]
public class HealthController : MonoBehaviour
{
    private static HealthController instance;

    [Header("Size Delta")]

    [Header("Bar Pixel Size")]
    [SerializeField] private int m_maxBar;
    [SerializeField] private int m_curBar;

    private Slider m_slider;
    private RectTransform m_rt;

    public static HealthController GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            m_slider = GetComponent<Slider>();
            m_rt = GetComponent<RectTransform>();
            
            m_slider.minValue = 0;
            m_slider.wholeNumbers = true;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }        
    }

    public void SetMax(int value)
    {
        m_maxBar = value;
        m_slider.maxValue = m_maxBar;
    }
    
    public void AddValue(int value)
    {
        m_curBar += value;
        if (m_curBar > m_maxBar)
        {
            m_curBar = m_maxBar;
        }
        else if (m_curBar < 0)
        {
            m_curBar = 0;
        }
        m_slider.value = m_curBar;
    }

    public void SubValue(int value)
    {
        AddValue(-value);
    }
    
    public void SetValue(int value)
    {
        m_curBar = value;
        if (m_curBar > m_maxBar)
        {
            m_curBar = m_maxBar;
        }
        else if (m_curBar < 0)
        {
            m_curBar = 0;
        }
        m_slider.value = m_curBar;
    }
}
