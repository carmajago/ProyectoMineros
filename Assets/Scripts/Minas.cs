using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;



public class Minas : MonoBehaviour {

	public Dictionary<int,Dictionary<int,Dictionary<int,string>>> mapas;
    [HideInInspector]
    public string json;
    public int contador;

    public static Minas mina;

    private void Awake()
    {
        if (mina == null)
        {
            mina = this;
            DontDestroyOnLoad(gameObject);
        }else if (mina != this)
        {
            Destroy(gameObject);
        }
       
        mapas = new Dictionary<int, Dictionary<int, Dictionary<int, string>>>();
        contador = 0;
    }

  


   
    


    public string GetJson()
    {
        json = JsonConvert.SerializeObject(mapas, Formatting.Indented);
        
        return json;
    }

}






