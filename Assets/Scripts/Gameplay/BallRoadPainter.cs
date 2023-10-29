using UnityEngine ;
using DG.Tweening ;
using System.Collections.Generic ;

public class BallRoadPainter : MonoBehaviour 
{
   [SerializeField] private LevelManager levelManager ;
   [SerializeField] private BallMovement ballMovement ;
   [SerializeField] private MeshRenderer ballMeshRenderer ;

   public int paintedRoadTiles = 0 ;

   private void Start () 
   {
      //paint ball:
      ballMeshRenderer.material.color = levelManager.paintColor ;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<RoadTile>())
      {
         RoadTile roadTile = other.GetComponent<RoadTile>();

         if (!roadTile.isPainted)
         {
            roadTile.GrassTile.transform.DOScale(0, 0.2f);
            roadTile.Particles.Play();
            
            roadTile.isPainted = true;
            
            if (!roadTile.isTrap)
               paintedRoadTiles++;

            if (paintedRoadTiles == levelManager.roadTilesList.Count) 
               StartCoroutine(GameManager.instance.Win());
         }
      }
   }
}
