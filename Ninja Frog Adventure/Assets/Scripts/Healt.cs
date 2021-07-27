using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healt : MonoBehaviour
{
    public int health;

    public int heartsCount;

    public Image[] heaths;
    public Sprite fullHeart;
    public Sprite noHeart;


    private void Update()
    {
        for(int i = 0; i < heaths.Length; i++)
        {
            if (i < health)
            {
                heaths[i].sprite = fullHeart;
            }
            else
            {
                heaths[i].sprite = noHeart;
            }

            if(i < heartsCount)
            {
                heaths[i].enabled = true;
            }
            else
            {
                heaths[i].enabled = false;
            }
        }
    }
}
