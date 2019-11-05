using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : GameObjectParent
{

    /***** Variables *****/

    /* -- Private -- */
    private Animator chestAnim;         /* animator for the chest */

    /* -- Public -- */
    public Item chestContents;          /* what's inside the chest */
    public bool isOpen;                 /* whether the chest is open or not */
    public ScriptableSignal raiseItem;  /* signal for the item */
    public Inventory playerInventory;   /* the player's inventory */
    // TODO implement dialog showing up when item is received
    //public GameObject dialogWindow;     /* TODO window for the item description */
    //public Text dialogText;             /* TODO what the dialog says */


    /***** Functions *****/

    // Start is called before the first frame update
    void Start()
    {
      /* get the animator for the chest */
      chestAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      /* check for the player pressing space and in range of the chest */
      if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
      {
        /* if the chest isn't opened, we want to open it */
        if(isOpen == false)
        {
          openChest();
        }
        /* else, chest is already opened */
        else
        {
          cannotOpenChest();
        }
      }


    }

    public void openChest()
    {
      // TODO dialogWindow.SetActive(true);
      // TODO dialogText.text = contents.itemDescription;

      /* add contents of the chest to the player's inventory */
      playerInventory.addItem(chestContents);
      playerInventory.currentItem = chestContents;

      /* raise the signal that the item is being grabbed */
      raiseItem.raise();

      /* set the chest as opend */
      isOpen = true;

      //TODO context.raise();

    }

    public void cannotOpenChest()
    {
      // TODO dialogWindow.SetActive(false);

      /* set current item to empty */
      playerInventory.currentItem = null;

      /* tell the signal to stop being raised (mainly for animation) */
      raiseItem.raise();

    }

}
