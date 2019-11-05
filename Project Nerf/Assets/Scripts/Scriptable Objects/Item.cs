using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{

  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public Sprite itemSprite;       /* sprite of the item */
  public string itemDescription;  /* description of the item, used when displaying dialog */
  public bool isKey;              /* whether the item is a key or not */


  /***** Functions *****/

}
