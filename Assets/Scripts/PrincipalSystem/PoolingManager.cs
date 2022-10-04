using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PooledItems
{
    public string Name;
    public string tag;
    public GameObject objectsToPool;
    public int amount;
}

public class PoolingManager : MonoBehaviour
{

    private static PoolingManager _instance;
    public static PoolingManager Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolingManager>();

            }
            return _instance;

        }
    }

    [SerializeField]
    private List<PooledItems> poolList = new List<PooledItems>();


    [SerializeField]
    private Dictionary<string, List<GameObject>> _items =
        new Dictionary<string, List<GameObject>>();    
    
    [SerializeField]
    private Dictionary<string, List<string>> _tags =
        new Dictionary<string, List<string>>();


    private void Awake()
    {
        for(int i = 0; i < poolList.Count; i++) // Para cada lista de objetos
        {
            PooledItems l = poolList[i];
            _items.Add(l.Name, new List<GameObject>()); // Creamos una entrada de
                                                        //el Dictionary
            string[] tagsSplit = l.tag.Split('/');

            for (int x = 0; x < tagsSplit.Length; x++)
            {
                if (!_tags.ContainsKey(tagsSplit[x]))
                {
                    _tags.Add(tagsSplit[x], new List<string>());
                }
                
                _tags[tagsSplit[x]].Add(l.Name);
            }

            for(int j = 0; j < l.amount; j++) // y añadimos las copias
            {
                GameObject tmp;
                tmp = Instantiate(l.objectsToPool); // Aqui creamos la copia
                tmp.transform.SetParent(transform); // ponemos los objetos como hijos
                                                    // del poolingObject
                tmp.SetActive(false); // la desactivamos 
                _items[l.Name].Add(tmp); // y la añadimos a la lista
            }
        }
    }

    IEnumerable<GameObject> ObjetoPool(int num, string name)
    {
        List<GameObject> tmp = _items[name];

        for (int i = 0; i < tmp.Count; i++)
        {
            if (!tmp[i].activeInHierarchy)
            {
                yield return tmp[i];
                num--;
                if (num <= 0) break;
            }
        }
    }

    public GameObject GetPooledObject(string name)
    { // Busca un objeto que esté desactivado y lo retorna
        List<GameObject> tmp = _items[name];

        for (int i = 0; i < tmp.Count; i++)
        {
            if (!tmp[i].activeInHierarchy)
            {
                return tmp[i];
            }
        }
        return null;
    }    
    
    public GameObject GetPooledObjectByTag(string tag)
    { // Busca un objeto que esté desactivado y lo retorna
        List<GameObject> tmp = _items[_tags[tag][UnityEngine.Random.Range(0 , _tags[tag].Count())]];
        //Debug.Log(_tags[tag][UnityEngine.Random.Range(0, _tags[tag].Count())]);
        for (int i = 0; i < tmp.Count; i++)
        {
            if (!tmp[i].activeInHierarchy)
            {
                return tmp[i];
            }
        }
        return null;
    }
}
