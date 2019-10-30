/**
 *  @file HeartManager.cs
 *
 *  @brief Defines the way the player's hearts on the screen work.
 *          This script doesn't have to be attached to the player to work
 *          because of the signal system we have set up. The heart container
 *          object listens for signals and calls updateHearts when it needs to
 *          update
 *
 *  @author: Alex Baker
 *  @date:   October 6 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */


    /* -- Public -- */
    public Image[] hearts;              /* array of images of hearts */
    public Sprite fullHeart;            /* full heart sprite */
    public Sprite halfHeart;            /* half full heart sprite */
    public Sprite emptyHeart;           /* empty heart sprite */
    public FloatValue heartContainers;  /* how many containers there are */
    public FloatValue playerCurrHealth; /* player's current health */

    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     */
    void Start()
    {
      Debug.Log("heartmanager start called");

      initHearts();
    }

    /**
     * initHearts()
     *
     * Initializes the heart container's game objects on the screen and sets
     *  them at the full heart sprite to start
     */
    public void initHearts()
    {
      Debug.Log("heartmanager init called");

      for(int i = 0; i < heartContainers.initialValue; i++)
      {
        hearts[i].gameObject.SetActive(true);
        hearts[i].sprite = fullHeart;
      }
    }

    /**
     * updateHearts()
     *
     * Checks the player's HP and updates the heart sprites based on how much
     *  HP they have left
     */
    public void updateHearts()
    {
      Debug.Log("Update hearts called");

      /* dividing by two because we want to take half hearts into consideration */
      float tempHP = playerCurrHealth.runtimeValue / 2;

      /* loop through the containers comparing the player's current HP to the
          value shown in the heart container */
      for(int i = 0; i < heartContainers.initialValue; i++)
      {
        if(i <= tempHP - 1)
        {
          Debug.Log("full heart called");

          /* set the heart to full */
          hearts[i].sprite = fullHeart;
        }
        else if(i >= tempHP)
        {
          Debug.Log("empty heart called");
          /* set the heart to empty */
          hearts[i].sprite = emptyHeart;
        }
        else
        {
          Debug.Log("half heart called");
          /* set the heart to half */
          hearts[i].sprite = halfHeart;
        }
      }
    }


}
