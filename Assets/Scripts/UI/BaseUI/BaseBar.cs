using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBar : MonoBehaviour
{
    [SerializeField] protected Slider slider;

    protected void SetValue(float current, float max)
    {
        slider.value = current / max;
    }
}