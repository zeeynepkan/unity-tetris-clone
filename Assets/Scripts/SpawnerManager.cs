using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] ShapeManager[] allShapes;

// bu fonksiyona buradan ulaşmayacağımız için Start fonk sildik GameManager içinden ulaşacağız 
   // private void Start()
       // {
          //  CreateFunctionFNC();
    
        //}
    
    
        public ShapeManager CreateFunctionFNC()
    {
        int randomShape = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(allShapes[randomShape],transform.position,Quaternion.identity) as ShapeManager;


        if (shape != null)
        {
            return shape;
        }
        else
        {
            print(message:"empty array");
            return null;

        }
    

    }

 }



    
     

