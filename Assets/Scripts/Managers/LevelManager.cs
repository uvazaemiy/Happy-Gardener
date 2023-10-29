using UnityEngine ;
using System.Collections.Generic ;

public class LevelManager : MonoBehaviour {
   [Header ("Level texture")]
   [SerializeField] private Texture2D levelTexture ;

   [Header ("Tiles Prefabs")]
   [SerializeField] private GameObject prefabWallTile ;
   [SerializeField] private GameObject prefabRoadTile ;
   [SerializeField] private GameObject prefabTrapTile ;

   [Header ("Ball and Road paint color")]
   public Color paintColor ;

   [HideInInspector] public List<RoadTile> roadTilesList = new List<RoadTile> () ;
   [HideInInspector] public RoadTile defaultBallRoadTile ;


   private Color colorWall = Color.black ;
   private Color colorRoad = Color.white ;
   private Color colorSpawn = Color.green;
   private Color colorTrap = Color.red;

   private float unitPerPixel ;


   private void Awake () 
   {
      Generate () ;
      //assign first road tile as default poition for the ball:
   }

   private void Generate () 
   {
      unitPerPixel = prefabWallTile.transform.lossyScale.x ;
      float halfUnitPerPixel = unitPerPixel / 2f ;

      float width = levelTexture.width ;
      float height = levelTexture.height ;

      Vector3 offset = (new Vector3 (width / 2f, 0f, height / 2f) * unitPerPixel)
                       - new Vector3 (halfUnitPerPixel, 0f, halfUnitPerPixel) ;

      for (int x = 0; x < width; x++) 
      {
         for (int y = 0; y < height; y++) 
         {
            //Get pixel color :
            Color pixelColor = levelTexture.GetPixel (x, y) ;

            Vector3 spawnPos = ((new Vector3 (x, 0f, y) * unitPerPixel) - offset) ;

            if (pixelColor == colorWall)
               Spawn (prefabWallTile, spawnPos) ;
            else if (pixelColor == colorRoad)
               Spawn (prefabRoadTile, spawnPos) ;
            else if (pixelColor == colorTrap)
               Spawn (prefabTrapTile, spawnPos) ;
            else if (pixelColor == colorSpawn)
               defaultBallRoadTile = Spawn (prefabRoadTile, spawnPos, true);
         }
      }
   }

   private RoadTile Spawn (GameObject prefabTile, Vector3 position, bool spawn = false) 
   {
      //fix Y position:
      position.y = prefabTile.transform.position.y ;

      GameObject obj = Instantiate (prefabTile, position, Quaternion.identity, transform) ;
      RoadTile roadTile = obj.GetComponent<RoadTile>();

      if (spawn)
         obj.GetComponent<RoadTile>().GrassTile.gameObject.SetActive(false);
      
      if (prefabTile == prefabRoadTile)
      {
         roadTilesList.Add (roadTile);
         return obj.GetComponent<RoadTile>();
      }
      else if (prefabTile == prefabTrapTile)
      {
         int trapSpriteIndex = Random.Range(0, roadTile.TrapSprites.Length);
         roadTile.GrassTile.sprite = roadTile.TrapSprites[trapSpriteIndex];
      }

      return null;
   }
}
