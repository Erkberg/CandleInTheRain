﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int id = 1;
    [TextArea]
    public string name;
    [TextArea]
    public string interactText;
    public string cantInteractText = "I'm missing something...";
    public int needsItemId = -1;
    public int replaceItemId = -1;
    public bool activeAtStart = true;
    public bool finishedAfterwards = false;
}
