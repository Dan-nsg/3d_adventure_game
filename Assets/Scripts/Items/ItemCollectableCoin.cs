using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableCoin : ItemCollectableBase
{
  public Collider2D collider;

  protected override void OnCollect()
  {
    base.OnCollect();
    ItemManager.Instance.AddByType(ItemType.COIN);
    collider.enabled = false;
  }
}
