using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GuardarYcargar : MonoBehaviour {

	
    public void Guardar()
    {
        Minas minas = GameObject.FindGameObjectWithTag("Minas").GetComponent<Minas>();

        var path = EditorUtility.SaveFilePanel(
               "Save texture as PNG",
               "",
                "Mina.json",
               "json");

        File.WriteAllText(path,minas.GetJson());
    }
    public void Cargar()
    {
        string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "json");

        Debug.Log(path);
        string readText = File.ReadAllText(path);

        var json = JsonConvert.SerializeObject(readText);
       // Dictionary<int, Dictionary<int, Dictionary<int, string>>> mapa;

       //mapa = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, Dictionary<int, string>>>>(json);

        
    }
}
