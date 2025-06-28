using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestDialogue", menuName = "Scriptable Objects/TestDialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(10, 5)]
    public List<string> dialogueSequence = new List<string>();
    public int inx = 0;
}
