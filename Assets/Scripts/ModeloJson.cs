using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ModeloJson : MonoBehaviour {

    public int mineros_oro;
    public int mineros_plata;
    public int mineros_cobre;
    public int mineros_comodin;

    public Dictionary<int, Dictionary<int, Dictionary<int, string>>> mapas;
   
}
