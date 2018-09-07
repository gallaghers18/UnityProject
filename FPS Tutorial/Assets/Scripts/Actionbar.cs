using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actionbar : MonoBehaviour {

    private Image[] children;
    private List<Image> actionButtonList = new List<Image>();
    private List<Image> frameList = new List<Image>();
    private List<Image> selectedList = new List<Image>();

    private int currentSelect; // should stay 0 through 8

    void Start () {
        //List of children indexed 1 = ActionButton, 2 = Frame, 3 = Selected, ... , etc
        //Note: index 0 = Actionbar itself
        children = gameObject.GetComponentsInChildren<Image>();

        //Split up into seperate lists
        for (int i = 0; i < 27; i=i+3)
        {
            actionButtonList.Add(children[i + 1]);
            frameList.Add(children[i + 2]);
            selectedList.Add(children[i + 3]);
        }

        currentSelect = 0;
        selectedList[currentSelect].enabled = true;
        
	}
	
	void Update () {

        // Scroll Wheel to move along action bars (Could turn into seperate method?)
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedList[currentSelect].enabled = false;
            currentSelect = ((currentSelect + 1) % 9 + 9) % 9;
            selectedList[currentSelect].enabled = true;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedList[currentSelect].enabled = false;
            currentSelect = ((currentSelect - 1) % 9 + 9) % 9;
            selectedList[currentSelect].enabled = true;
        }

        // Number key shortcut to move along action bars
        for (int i = 1; i < 10; ++i)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedList[currentSelect].enabled = false;
                currentSelect = i-1;
                selectedList[currentSelect].enabled = true;
            }
        }


    }

    public int GetCurrentSelect()
    {
        return currentSelect;
    }
}
