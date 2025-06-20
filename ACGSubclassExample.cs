using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity.Custom;

/// <summary>
/// This class is an example of how AudioControlledGameplay could be subclassed to control a title screen.
/// </summary>
public class ACGSubclassExample : AudioControlledGameplay
{
    private void Awake()
    {
        BuildCueList(
            ShowTopText, // on the first accented note
            ShowBottomText, // on the second accented note
            ShowPlayButton // on the third accented note
        );
    }

    private void ShowTopText()
    {

    }

    private void ShowBottomText()
    {

    }

    private void ShowPlayButton()
    {

    }
}
