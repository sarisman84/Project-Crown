using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;

    
    void Start()
    {
        ObjectPooler.PoolObject(prefab, 1, transform);
        StartCoroutine(Tester());
    }

    private void Update() {
       if(Input.GetKeyDown(KeyCode.Space)){
            GameObject obj = ObjectPooler.Dynamic_GetPooledObject(prefab);
            obj.SetActive(true);
            obj.transform.position = new Vector2(UnityEngine.Random.Range(-10, 11), UnityEngine.Random.Range(-10, 11));
        }
    }

    // Update is called once per frame
    IEnumerator Tester(){
        while (true)
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
            int i = UnityEngine.Random.Range(0, objs.Length);
            if(objs[i].activeInHierarchy && objs[i].GetComponent<Camera>() == null && objs[i].GetComponent<OPTest>() == null) objs[i].SetActive(false);
                
            
            yield return new WaitForSeconds(1.5f);
        }
        
    }
  
}
