using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Pickable
{
    protected override void OnPicked() {
        GameController.createCompanion();
    }
}
