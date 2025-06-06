using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspirationBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Inspiration.Instance.SetInspirationSlider(GetComponent<Slider>());
        Inspiration.Instance.ResetCurrentInspiration();
    }


}
