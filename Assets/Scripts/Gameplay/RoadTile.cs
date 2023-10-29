using UnityEngine ;

public class RoadTile : MonoBehaviour 
{
   public MeshRenderer meshRenderer ;
   public Vector3 position ;
   public ParticleSystem Particles;
   public Sprite[] TrapSprites;
   public SpriteRenderer GrassTile;
   [Space]
   public bool isPainted ;
   public bool isTrap;

   private void Awake ()
   {
      position = transform.position ;
      isPainted = false ;
   }
}
