using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOnOffManager : MonoBehaviour
{
    public Sprite openIcon;
    public Sprite closeIcon;
    private Image iconImg;

    public bool defaultIconStatus = true;//başlangıçtaki ıconun açık olması gerekli


    private void Start()
    {
        iconImg = GetComponent<Image>();//resim yükleyeceğimiz alanıçekmiş olduk


        iconImg.sprite = (defaultIconStatus) ? openIcon : closeIcon;
        
        /*if(defaultIconStatus)
        {
            iconImg.sprite = openIcon;
        }
        else
        {
            iconImg.sprite = closeIcon;
        }*/ //bu if else bloğu yerine tek satırlık kod yazabiliriz yukarıda yazdığım gibi aynı ifade

    
    }


     public void iconOnOffFNC(bool iconStatus)
        {
            if(!iconImg || !openIcon || !closeIcon)
            {
                return;
            }

            iconImg.sprite = (iconStatus) ? openIcon : closeIcon;
        }



}
