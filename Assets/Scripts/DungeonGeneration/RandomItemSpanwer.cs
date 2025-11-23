using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField]
    int stepNumbers;

    public enum RoomSize // What Is The Current Room Size?
    {
      Small,
      Medium,
      Large

    }
     
    [SerializeField]
    private RoomSize roomSize; // Choose in The Inspector The Room Size.
    
    public LayerMask environment, obstacles, doors, doorsCrates;
    public List<GameObject> objectToSpawn;
    public bool isThereWall, wallsAround, canSpawnOtherItems;
    public List<GameObject> otherToSpawn;
    bool hasFinished;
    int numberofStepsAlready , numberOfEnemyAlready;
    public float CircleRange;
    public List<int> MoveDirection;
    public float RayLengthUp , RayLengthDown , RayLengthRight, RayLengthLeft;
    public int RandDir , RandSpawnObject , RandOtherToSpawn , chanceToSpawn; //Dir 0 IS Up, Dir 1 Is Down , Dir 2 Is Right , Dir 3 , Is Left 

    bool hasEnvironmentObject, canSpawnCrates;
    RaycastHit2D upHit2DWall, upDetectOtherDoors; 
        
    RaycastHit2D downHit2DWall, downDetectOtherDoors;
        
    RaycastHit2D rightHit2DWall, rightDetectOtherDoors;

    RaycastHit2D leftHit2DWall, leftDetectOtherDoors;

    RaycastHit2D noSpawnCratesDown, noSpawnCratesUp;

    bool hasFirstMoveYet = false;

    bool onlyFloor;

    bool hasFinishedSpawning, upOrDownRay; // Has The Game Object Finished Spawning?

    int itemChanceOrder = -1;

    int EnemyChanceOrder;
    
    [Header("EnemiesPerRoom")]
    public int EnemyPerSpawner;
    public int maxEnemyPerSpawner;
    
    [Header("LootPerRoomSize")]
    public int maxLootSmall;
    public int maxLootMedium; 
    public int maxLootLarge;

    private int currentMaxLoot, lootCounter;

    [Header("EnemiesChances")]

    public int slimeChance;
    public int ratChance;
    public int batChance;
    public int goblinChance;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        maxLootSmall = Random.Range(1,3);
        maxLootMedium = Random.Range(2,6);
        maxLootLarge = Random.Range(3,8);

        RoomSizeCurrent();

        stepNumbers = 100;

        for (int i = 0; i < stepNumbers; i++)
        {

          InvokeRepeating("Walk_Randomly",2,0);

        }


    StartCoroutine("ScanWait");
     
    }
   
    void Walk_Randomly()
    {
      DetectRayCast();
      SpawningCrates();
      SpawnEnemies();
      SpawnOther();
      SwitchingDirectionIfThereObstacles(); //Checks The Next Step For The GameObject.
      
    }


    void DetectRayCast()
   {

    // Check if there is any object in the environment within the circle range
     hasEnvironmentObject = Physics2D.OverlapCircle(transform.position, CircleRange, environment);

    // Perform raycasts in each direction to detect walls
     upHit2DWall = Physics2D.Raycast(transform.position, Vector2.up, RayLengthUp, obstacles);
     downHit2DWall = Physics2D.Raycast(transform.position, Vector2.down, RayLengthDown, obstacles);
     rightHit2DWall = Physics2D.Raycast(transform.position, Vector2.right, RayLengthRight, obstacles);
     leftHit2DWall = Physics2D.Raycast(transform.position, Vector2.left, RayLengthLeft, obstacles);
     
     //Preform RayCast To Detect Doors To Prevent Chests and other Items from Blocking The DoorWay.
     upDetectOtherDoors = Physics2D.Raycast(transform.position, Vector2.up, RayLengthUp, doors);
     downDetectOtherDoors = Physics2D.Raycast(transform.position, Vector2.down, RayLengthDown, doors);
     rightDetectOtherDoors = Physics2D.Raycast(transform.position, Vector2.right, RayLengthRight, doors);
     leftDetectOtherDoors = Physics2D.Raycast(transform.position, Vector2.left, RayLengthLeft, doors);

     //Preform RayCast To Prevent Crates From Blocking Doorway.
     noSpawnCratesUp = Physics2D.Raycast(transform.position, Vector2.up, RayLengthUp, doorsCrates);
     noSpawnCratesDown = Physics2D.Raycast(transform.position, Vector2.down, RayLengthDown, doorsCrates);

    // Check if there are walls detected in any direction
     wallsAround = (upHit2DWall && downHit2DWall || rightHit2DWall && leftHit2DWall);

     canSpawnCrates = (!noSpawnCratesDown && !noSpawnCratesUp);

    // Check if there are no Doors above or below for spawning other items
     canSpawnOtherItems = (!upDetectOtherDoors && !downDetectOtherDoors && !rightDetectOtherDoors && !leftDetectOtherDoors);
     
     // Check If No Walls Around.
     onlyFloor = (!upHit2DWall && !downHit2DWall && !rightHit2DWall && !leftHit2DWall);
     

     //Check If There is Wall Above Or Below To Spawn Crates.
     upOrDownRay = (upHit2DWall || downHit2DWall);

   }

    void SwitchingDirectionIfThereObstacles()
    {

      DetectRayCast();

      if(!hasFirstMoveYet) //First Move For The Spawner.
      {

        ChangeDirection();

        hasFirstMoveYet = true; 
        
      }

     
      if(RandDir == 0 && upHit2DWall) // if There Is Walls Above Change Direction. 0 is For Moving Up.
      {
    
        ChangeDirection();
        return;
      }


      else if(RandDir == 1 && downHit2DWall) // if There Is Walls Down Change Direction. 1 is For Moving Down.
      {
        
        ChangeDirection();
        return;
      }

       
      else if(RandDir == 2 && rightHit2DWall) // if There Is Walls Right Change Direction. 2 is For Moving Right.
      {
        
        ChangeDirection();
        return;
      }

       
      else if(RandDir == 3 && leftHit2DWall) // if There Is Walls Left Change Direction. 3 is For Moving Left.
      {
        
        ChangeDirection();
        return;
      } 
     
      MoveTheSpawner();

    }

    void ChangeDirection()
    {
      
      // Generate A new Direction.
      RandDir = Random.Range(0,MoveDirection.Count);

    }

    void MoveTheSpawner()
    {
      

      if(RandDir == 0)
      {
        transform.position = new Vector2(transform.position.x,transform.position.y + 1); // move above.
      }

      else if(RandDir == 1)
      {
        transform.position = new Vector2(transform.position.x,transform.position.y - 1); // move Down.
      }

      else if(RandDir == 2)
      {
        transform.position = new Vector2(transform.position.x + 1,transform.position.y);
      }

      else if(RandDir == 3)
      {
        transform.position = new Vector2(transform.position.x - 1,transform.position.y);
      }

    }

    void SpawningCrates()
    {
      DetectRayCast();
      CantSpawn();

      if(!canSpawnCrates)
      {
         return;
      }


      if(CantSpawn() == true)
      {
          return;
      }


        chanceToSpawn = Random.Range(0, 101);

        if(chanceToSpawn <= 25 && upOrDownRay)
        {
          
          RandSpawnObject = Random.Range(0,objectToSpawn.Count);

          Instantiate(objectToSpawn[RandSpawnObject],transform.position,Quaternion.identity);

        }


      
    }


    void SpawnOther()
    {

       DetectRayCast();
       CantSpawn();
       

       if(!canSpawnOtherItems)
       {
         return;
       }

       if(CantSpawn() == true )
       {
         return;
       }

       
       int OtherChances = Random.Range(0,101 + ItemCounter.Instance.LuckStat * 2);
        
       List<int> ItemsTable = new List<int> {51,20,18,12}; //50% to spawn nothing per Tile.

       foreach(int Chance in ItemsTable)
       {
         if(Chance <= OtherChances)
         {

           OtherChances -= Chance; //34,firstItemChance - (34),OtherChances = 0, 40 <= 0 ?; so the 40 Chance Item Will Spawn !
           
           if(itemChanceOrder < otherToSpawn.Count)
           {

             itemChanceOrder++;

           }
           
         }
         
         else
         {

             if(itemChanceOrder <= otherToSpawn.Count && itemChanceOrder > -1 )
             {

                if(lootCounter < currentMaxLoot)
                {

                  lootCounter++;

                }

                else
                {

                  itemChanceOrder = -1;

                  break;

                }

                Instantiate(otherToSpawn[itemChanceOrder], transform.position, Quaternion.identity);
               
             }
           
           itemChanceOrder = -1;

           break;
           
         } 

       } 
       

    }


    void SpawnEnemies()
    {

       DetectRayCast();
       CantSpawn();

       int RandIgnoreSpawn = Random.Range(1,101);

       if(RandIgnoreSpawn <= 70)
       {
         return;
       } 

       if(!canSpawnOtherItems)
       {
         return;
       }

       if(CantSpawn() == true )
       {
         return;
       }

       if(EnemyPerSpawner == maxEnemyPerSpawner)
       {
         return;
       }

       int EnemyChancePerTile = Random.Range(1,201);

       List<int> EnemiesTable = new List<int> {slimeChance, ratChance, batChance, goblinChance};

       foreach(int enemychance in EnemiesTable)
       {
          if( enemychance <= EnemyChancePerTile )
          {

            EnemyChancePerTile -= enemychance;
            EnemyChanceOrder++;
            
          }

          else
          {
            
            Instantiate(enemies[EnemyChanceOrder],transform.position,Quaternion.identity);

            EnemyPerSpawner++;

            EnemyChanceOrder = 0;

            break;

          } 
        

       }
      
    }


      bool CantSpawn()
      {
            
        if(wallsAround || hasEnvironmentObject)
        {
          return true;
        }
        else
        {
          return false;
        }

      }

    IEnumerator ScanWait()
    {
        yield return new WaitForSeconds(3.5f);
         AstarPath.active.Scan();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y + RayLengthUp));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y - RayLengthDown));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + RayLengthRight,transform.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x - RayLengthLeft,transform.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,CircleRange);
    }

    void RoomSizeCurrent()
    {
       switch(roomSize)
       {
          case RoomSize.Small:
            maxEnemyPerSpawner = Random.Range(1, 4);
            currentMaxLoot = maxLootSmall;
            break;

          case RoomSize.Medium:
          maxEnemyPerSpawner = Random.Range(2, 5);
          currentMaxLoot = maxLootMedium;
          break;

          case RoomSize.Large:
          maxEnemyPerSpawner = Random.Range(3, 8);
          currentMaxLoot = maxLootLarge;
          break;

       }

      
    }
}
