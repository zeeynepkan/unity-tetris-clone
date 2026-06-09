using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShapeManager : MonoBehaviour
{
    private ShapeManager followShape = null; //bir takip shape nesnesi oluşturuyoruz


    bool isTouchGround = false; //yere değip değmediğni kontrol ediyoruz yere değdiyse kaldıracak ekrandan


    public Color color = new Color(1f,1f,1f,.2f);


    public void CreateFollowShapeFNC(ShapeManager realShape, BoardManager board)

    {
        if(!followShape)
        {
            followShape=Instantiate(realShape,realShape.transform.position,realShape.transform.rotation) as ShapeManager;
            followShape.name = "FollowShape";

            SpriteRenderer[] allSprite = followShape.GetComponentsInChildren<SpriteRenderer>();


            foreach(SpriteRenderer sr in allSprite)
            {
                sr.color = color;
            }
        }

        isTouchGround = false;

        

    }
}
