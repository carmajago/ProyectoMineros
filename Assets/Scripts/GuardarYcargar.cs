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
}
