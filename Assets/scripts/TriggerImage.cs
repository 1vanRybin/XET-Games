using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerImage : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text LowChargeText;

    private void Start()
    {
        image.enabled = false;
        LowChargeText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ChargeBatteryTrigger.count > 10)
            {
                ChargeBatteryTrigger.count -= 10;
                image.enabled = true;
            }
            else LowChargeText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            image.enabled = false;
            LowChargeText.enabled = false;
        }
    }

}
