using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    void Awake(){
        if(Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    } 

    public Transform content;
    public GameObject popUp;

    public List<Button> sensorButtons = new List<Button>();
    public List<Slider> sliders = new List<Slider>();


    [Header("Warning Sprites")]
    public Sprite sensoryOverloadSprite;
    public Sprite signalAreaSpite;
    public Sprite radiationSprite;
    public Sprite controlLostSprite;
    public Sprite highTemperatureSprite; 

    public void Disable(){
        foreach(Button button in sensorButtons){
            button.interactable = false;
            button.gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f, 
                button.gameObject.GetComponent<Image>().color.a);
        }

        foreach(Slider slider in sliders){
            slider.gameObject.SetActive(false);
        }
    }

    public void Enable(){
        foreach(Button button in sensorButtons){
                button.interactable = true;
                button.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f,
                    button.gameObject.GetComponent<Image>().color.a);
        }
    }

    public GameObject CreateWarning(Sprite sprite){
        GameObject obj = Instantiate(popUp);
        obj.GetComponent<Image>().sprite = sprite; 
        obj.transform.SetParent(content);
        return obj;
    }
}
