using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : TalkManager
{

    protected override void GenerateData()
    {
        talkData.Add(1000, new string[] { "¾È³ç." });
    }
}
