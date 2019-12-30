using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OffensivePlayerSystems : MonoBehaviour
{
    //What does this class do?

    //TODO  Basic projectile shooting
    //TODO  Object Pooler for projectile shooting
    //TODO  Projectile definitions (like who fired it, what behaivour does the projectile do, etc.)


    public GameObject indicatorPrefab;

    //This class is where player input translates into logic (i.e. aiming)



   AimingLogic aim;
    private void Awake() {
        aim = new AimingLogic(gameObject);
        aim.AffectGameObject(GameObject.Find(indicatorPrefab.name));
    }

    private void Update() {
        aim.TowardsCursor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }




}



public class AimingLogic {


   
    Transform transform;
    Quaternion rotation;
    Vector2 position;


    List<GameObject> listOfGameObjects = new List<GameObject>();


    public bool SystemDisabled {private get; set;}

    public void AffectGameObject(GameObject gameObject){
        if(listOfGameObjects.Contains(gameObject)) return;
        listOfGameObjects.Add(gameObject);
    }

    public void ResetGameObject(GameObject gameObject){
        listOfGameObjects.Remove(gameObject);
    }

 

    public AimingLogic(GameObject _gameObject){
       
        transform = _gameObject.transform;
        position = new Vector2(transform.position.x, transform.position.y);
        
            
    }
    public Vector2 TowardsCursor(Vector2 mousePosition){
        if(listOfGameObjects.Count == 0 || !Array.Find(GameObject.FindObjectsOfType<GameObject>(), p => p == listOfGameObjects[UnityEngine.Random.Range(0, listOfGameObjects.Count)])) throw new NullReferenceException("Prefab missing in the scene.");
        Vector2 direction = -(position - mousePosition).normalized;
         if(SystemDisabled) return direction;
        for (int i = 0; i < listOfGameObjects.Count; i++)
        {
            GameObject obj = listOfGameObjects[i];
            obj.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }


        return direction;
    }

    

}
