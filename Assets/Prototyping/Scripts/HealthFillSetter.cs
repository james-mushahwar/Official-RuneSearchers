using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFillSetter : MonoBehaviour
{
    public FloatReference CurrentHealth;
    public float MaxHealth;

    public Image Image;

    void Start()
    {
        MaxHealth = CurrentHealth.Variable.OriginalValue;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Health now is " + CurrentHealth.Value);
        Image.fillAmount = Mathf.Clamp01(CurrentHealth.Value / MaxHealth);
    }
}
