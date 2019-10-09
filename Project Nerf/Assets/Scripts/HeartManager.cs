using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;              /* array of images of hearts */
    public Sprite fullHeart;            /* full heart sprite */
    public Sprite halfHeart;            /* half full heart sprite */
    public Sprite emptyHeart;           /* empty heart sprite */
    public FloatValue heartContainers;  /* how many containers there are */


    // Start is called before the first frame update
    void Start()
    {
      initHearts();
    }

    public void initHearts()
    {
      for(int i = 0; i < heartContainers.initialValue; i++)
      {
        hearts[i].gameObject.SetActive(true);
        hearts[i].sprite = fullHeart;
      }

    }

}
