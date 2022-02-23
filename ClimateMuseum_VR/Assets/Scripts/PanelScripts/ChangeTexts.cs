using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This scripts changes the displayed text in the tab "Key Dynamics" in the panels if the player clicks onto the "<" or ">" button.

public class ChangeTexts : MonoBehaviour
{
  // text tiles that will be presented subsequently
  public TextMeshProUGUI[] texts;
  
  // index for going through the tiles
  public int index = 0;

  // buttons to jump through the text tiles
  public Button LeftButton;
  public Button RightButton;

    void Awake()
  {
    // at the beginning, the left button (<) is inactive, the right button (>) is active
    LeftButton.interactable = false;
    RightButton.interactable = true;
  }


  void DisplayText(int index)
  {
    // for each text tile:   
    for (int i = 0; i < this.texts.Length; i++)
    {
      // display the one corresponding to the current index
      if (i == index)
      {
        this.texts[i].enabled = true;
      }
      // disable all others, so they will not be displayed
      else
      {
        this.texts[i].enabled = false;
      }
    }
  }

  // if the right button is clicked, the next tile is displayed
  public void DisplayNext()
  {
    // left button can now be used
    LeftButton.interactable = true;
    
    // if displayed text is already last one, keep it displayed and keep button not interactable
    if (index == texts.Length - 1)
    {
      index = texts.Length - 1;
      RightButton.interactable = false;
      //Debug.Log("right > button disabled");
    }

    // if displayed text is the second-last one, display the last one (next) and set button not interactable
    if (index == texts.Length - 2)
    {
      index += 1;
      RightButton.interactable = false;
      //Debug.Log("right > button disabled");
    }

    // if displayed text is the third-last or lower one, display next
    if (index < texts.Length - 2)
    {
      index += 1;
    }

    // display the text at the calculated index
    DisplayText(index);
  }

  // if the left button is clicked, the previous tile is displayed
  public void DisplayPrevious()
  {
    // right button can now be used
    RightButton.interactable = true;

    // if displayed text is already first one, keep it displayed and keep button to not interactable
    if (index == 0)
    {
      LeftButton.interactable = false;
      // Debug.Log("left < button disabled");
    }

    // if displayed text is the second one, display first one (previous) and set button to not interactable
    if (index == 1)
    {
      index -= 1;
      LeftButton.interactable = false;
      // Debug.Log("left < button disabled");
    }

    // if displayed text is the third or higher one, display previous
    if (index > 1)
    {
      index -= 1;
    }

    // display the text at the calculated index
    DisplayText(index);
  }

}
