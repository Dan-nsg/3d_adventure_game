using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemJump : ClothItemBase
    {
        public float jumpSpeed = 30f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeJump(jumpSpeed, duration);
        }

    }
}