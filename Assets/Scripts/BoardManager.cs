using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefabe;
    

    public int high = 22;
    public int width = 10;

    private Transform[,] grill;//her bir kare için ızgara oluşturuyor gelen bloklar üst üste eklenmesin diye

    public int completedRow = 0;


    private void Awake()
    {
      grill = new Transform[width, high];
    }


    private void Start()
    {
        CreateEmptySquareFNC();
    }

    bool IsInBoard(int x, int y)
    {
      return (x >= 0 && x < width && y >= 0);
    }

    bool IsSquareFull(int x, int y, ShapeManager shape)//eğer kare boşsa şekil inmeye devam edecek değilse gelmeyecek
    {
      return (grill[x, y] != null && grill[x, y].parent != shape.transform);
    }




    public bool IsPositionAcceptable(ShapeManager shape)
    {
      foreach (Transform child in shape.transform)
      {
        Vector2 pos = MakeTheVectorIntFNC((Vector2)child.position);


        if(!IsInBoard((int)pos.x,(int)pos.y))
        {
          return false;
        }

        if(pos.y < high)
        {
          if (IsSquareFull((int)pos.x, (int)pos.y, shape))//kare başka bir şekille doluysa inmeyecek
        {
          return false;
        }

        }

        
      }

      return true;
    }

    void CreateEmptySquareFNC()
    {
        if (tilePrefabe != null)
        {
             for(int y = 0; y < high; y++)
        {
          for(int x = 0; x < width; x++)
          {
            Transform tile = Instantiate(tilePrefabe, position:new Vector3(x, y, z:0), Quaternion.identity);
            tile.name = "x" + x.ToString() + "y" + y.ToString();
            tile.parent = this.transform;
          } 
        }
    }
    else{
        print(message:"tile prefab not added");
    }


        }

        public void ShapeIntoGrillFNC(ShapeManager shape)
        {
          if (shape == null)
            return;
          

          foreach (Transform child in shape.transform)
          {
              Vector2 pos = MakeTheVectorIntFNC((Vector2)child.position);

              grill[(int)pos.x, (int)pos.y] = child;
             // int x = (int)pos.x;
              //int y = (int)pos.y;
              //Grid bound kontrolü
              //if (x >= 0 && x < width && y >= 0)

               //Debug.Log("Grid Kaydı: x=" + pos.x + " y=" + pos.y + "   child=" + child.name);

              
                //  grill[x,y] = child;
          }
        }
      
      //satırın tamamlanıp tamamlanmadığına bakıyor eğer tamamlandıysa satırı silme işlemine geçeceğiz
        bool IsRowCompleteFNC(int y)
        {
          for (int x = 0; x < width; ++x)
          {
            if (grill[x,y] == null)
            {
              return false;
            }

          }

          return true;

        }

        void CleanRowFNC(int y)
        {
          for (int x = 0; x < width; ++x)
          {
            if (grill[x,y] != null)
            {
              Destroy(grill[x,y].gameObject);
            }

            grill[x,y] = null;
          }

        }
      //yukarıdaki kodla dolan satırı temizledikten sonra bir satır aşağı indirmeliyiz
        void DownOneRowFNC(int y)
        {
          for (int x = 0; x < width; ++x)
          {
            if (grill[x,y] != null)
            {
              grill[x,y-1] = grill[x,y];
              grill[x,y] = null;
              grill[x,y-1].position += Vector3.down;
            }
            
          }

        }


        //şimdi bir satır aşağı indirdikten sonra tüm satırları aşağı indirmeliyiz
        void DownAllRowFNC(int startY)
        {
          for (int i = startY; i < high; ++i)
          {
            DownOneRowFNC(i);

          }

        }

        public void CleanAllRowFNC()
        {
          completedRow = 0;


          for (int y = 0; y < high; y++)
          {
            if(IsRowCompleteFNC(y))
            {
              completedRow++;
              CleanRowFNC(y);
              DownAllRowFNC(startY:y+1);
              y--;
            }
          }
        }


        //Şeklin dışarı çıkıp çıkmadığını kontrol ediyor 
        public bool IsItOutFNC(ShapeManager shape)
        {
          foreach(Transform child in shape.transform)
           {
            if(child.transform.position.y >= high - 1)
             {
              return true;
             }
           }

          return false;
        }
        Vector2 MakeTheVectorIntFNC(Vector2 vector)
    {
        return new Vector2(x:Mathf.Round(vector.x),y:Mathf.Round(vector.y));

    }



}
