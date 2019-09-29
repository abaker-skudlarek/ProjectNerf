/**
 *  @file EnemyAI.cs
 *
 *  @brief Parent class for all enemies. Defines the basic characteristics of
 *          what every enemy should have
 *
 *  @author: Alex Baker
 *  @date:   September 29 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */
    public double enemyHealth;     /* the base health of the enemy */
    public string enemyName;       /* the name of the enemy */
    public double enemyBaseAttack; /* the base attack value of the enemy */
    public float enemyBaseSpeed;   /* the base move speed of the enemy */

    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {

    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     *
     */
    void Update()
    {

    }

}
