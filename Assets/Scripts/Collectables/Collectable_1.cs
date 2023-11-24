using PajamaNinja.AudioUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_1 : Collectable
{
    public override void PickUp()
    {
        AudioManager.Instance.PlayOneShot("coin");
        base.PickUp();
    }
}
