using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{

  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public Item currentItem;                    /* current item from chest */
  public List<Item> items = new List<Item>(); /* list of items the player has */
  public int keyCount;                        /* how many keys the player has */

  /***** Functions *****/

  public void addItem(Item itemAdded)
  {
    /* check if item is key */
    if(itemAdded.isKey == true)
    {
      /* increment key count by 1 when a key is found */
      keyCount++;
    }
    else
    {
      /* if the item isn't already in the item list, add it to the list */
      if(items.Contains(itemAdded) == false)
      {
        items.Add(itemAdded);
      }
    }
  }

}
