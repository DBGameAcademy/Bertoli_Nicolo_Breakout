using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBallsPower : Pickable
{
    protected override void OnPicked() {
        GameController.duplicateBalls();
    }
}
