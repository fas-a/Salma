using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
public Slider slider;
    public Image fillImage;
    private float timer = 100f;

    public void OnSliderChanged()
    {
        if (slider.value <= 50f)
        {
            fillImage.color = Color.Lerp(Color.green, Color.red, (50f - slider.value) * 2);
        } else
        {
            fillImage.color = Color.green;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (slider != null)
        {
            slider.maxValue = timer;
            slider.value = timer;

            fillImage.color = Color.green;

            slider.onValueChanged.AddListener(delegate { OnSliderChanged(); });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
