using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDamage : Pickable
{
    protected override void OnPicked() {
        GameController.DamageUp();
    }
}
