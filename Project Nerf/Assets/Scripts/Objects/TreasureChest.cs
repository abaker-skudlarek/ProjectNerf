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

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     */
    void Start()
    {
      /* get the animator for the chest */
      chestAnim = GetComponent<Animator>();
    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     */
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

    /**
     * openChest()
     *
     * Adds the contents of the chest to the player's inventory and raises the
     *  signal attached to it
     */
    public void openChest()
    {
      // TODO dialogWindow.SetActive(true);
      // TODO dialogText.text = contents.itemDescription;

      /* add contents of the chest to the player's inventory */
      playerInventory.addItem(chestContents);
      playerInventory.currentItem = chestContents;

      /* raise the signal that the item is being grabbed */
      raiseItem.raise();

      /* set the chest as opened */
      isOpen = true;

      /* set the bool to open the chest */
      chestAnim.SetBool("isOpened", true);

      //TODO context.raise();

    }

    /**
     * cannotOpenChest()
     *
     * Called when the chest has already been opened. Raises item's signal
     */
    public void cannotOpenChest()
    {
      // TODO dialogWindow.SetActive(false);

      /* tell the signal to stop being raised (mainly for animation) */
      raiseItem.raise();

    }

    /**
     * OnTriggerEnter2D(Collider2d)
     *
     * Executes when this object is triggered. This function will check to see if the
     *  player is in the trigger zone and set playerInRange = true if so
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to
     */
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
      if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger
          && isOpen == false)
      {
        /* set player in range to true */
        playerInRange = true;

        //TODO context.raise();

      }

    }

    /**
     * OnTriggerExit2D(Collider2d)
     *
     * Executes when this object is done being triggered. This will check to see
     *  is the player exits the trigger zone and set playerInRange = false if so
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to
     */
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
      if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger
          && isOpen == false)
      {
        /* set player in range to false */
        playerInRange = false;

        //TODO context.raise();
      }

    }

}
