using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ScaleSuperNova : MonoBehaviour {
    /**
     * A class that scales the super nova up
     * and down by time instead of instantly.
     **/
#if MS1
    protected VRTK_ControllerEvents controllerEvents;
    private GameObject superNovaLeft;
    private GameObject superNovaRight;
    public float scaleFactor;
    public float lowerBound;
    public float upperBound;

    void Awake()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        superNovaLeft = GameObject.FindGameObjectWithTag("SuperNovaLeft");
        superNovaRight = GameObject.FindGameObjectWithTag("SuperNovaRight");
    }

    // Update is called once per frame
    void Update () {
        if (superNovaLeft.activeSelf)
        {
            float size = superNovaLeft.transform.localScale.x;
            if (controllerEvents.buttonOnePressed && !(controllerEvents.buttonTwoPressed && size < upperBound))
            {
                superNovaLeft.transform.localScale += Vector3.one * Time.deltaTime * scaleFactor; ;
            }
            else if (controllerEvents.buttonTwoPressed && !(controllerEvents.buttonOnePressed) && size > lowerBound)
            {
                superNovaLeft.transform.localScale -= Vector3.one * Time.deltaTime * scaleFactor;
            }
        }
        else
        {
            float size = superNovaRight.transform.localScale.x;
            if (controllerEvents.buttonOnePressed && !(controllerEvents.buttonTwoPressed && size < upperBound))
            {
                superNovaRight.transform.localScale += Vector3.one * Time.deltaTime * scaleFactor; ;
            }
            else if (controllerEvents.buttonTwoPressed && !(controllerEvents.buttonOnePressed) && size > lowerBound)
            {
                superNovaRight.transform.localScale -= Vector3.one * Time.deltaTime * scaleFactor;
            }
        }
	}
#elif MS2
    protected VRTK_ControllerEvents controllerEvents;
    private GameObject superNova;
    public float scaleFactor;
    public float lowerBound;
    public float upperBound;

    void Awake()
    {
        superNova = GameObject.FindGameObjectWithTag("SuperNova");
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
    }

    // Update is called once per frame
    void Update()
    {
            float size = superNova.transform.localScale.x;
            if (controllerEvents.buttonOnePressed && !(controllerEvents.buttonTwoPressed && size < upperBound))
            {
                superNova.transform.localScale += Vector3.one * Time.deltaTime * scaleFactor; ;
            }
            else if (controllerEvents.buttonTwoPressed && !(controllerEvents.buttonOnePressed) && size > lowerBound)
            {
                superNova.transform.localScale -= Vector3.one * Time.deltaTime * scaleFactor;
            }
    }
#endif
}
