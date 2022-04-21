using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : Pickable
{
    protected override void OnPicked() {
        GameController.increasePaddleSize();
    }
}
