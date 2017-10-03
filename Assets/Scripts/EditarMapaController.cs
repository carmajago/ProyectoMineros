using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditarMapaController : MonoBehaviour
{

    public GameObject pared;
    public GameObject oro;
    public GameObject plata;
    public GameObject cobre;
    public GameObject plano;
    public GameObject mina;

    public List<Toggle> toggles;

    private int ancho;
    private int alto;
    Toggle activo;

    public Dictionary<int, Dictionary<int, string>> mapa;



    void Start()
    {
        mapa = new Dictionary<int, Dictionary<int, string>>();
        ancho = (int)plano.transform.localScale.x;
        alto = (int)plano.transform.localScale.z;
        CrearTerreno();

        activo = toggles[0];
    }

    public void LateUpdate()
    {

        ValidarToggles();

    }


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(0))
        {
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            EditarMapa(v3.z, v3.x);
        }
    }


    //Inicializa todo el terreno con rocas
    public void CrearTerreno()
    {
        for (int i = 0; i < alto; i++)
        {
            Dictionary<int, string> aux = new Dictionary<int, string>();
            for (int j = 0; j < ancho; j++)
            {

                GameObject temp = Instantiate(pared, new Vector3((j * 10) + 5, 0, (i * 10) + 5), Quaternion.identity);
                aux.Add(j, temp.name);
                temp.transform.parent = mina.transform;

            }
            //////Corregir
            mapa.Add(i, aux);
        }
    }

    //Quita un cubo del mapa
    //x,z son las posiciones del mouse
    public void EliminarElemento(int x, int z)
    {
        ///////////////Corregir metodo
            // Debug.Log(x_temp + "," + z_temp + "Object " + mapa[x_temp][z_temp]);
          //  Destroy(mapa[x][z]);
            mapa[x][z] = null;
        

    }
    public void ValidarToggles()
    {
      
        foreach (var item in toggles)
        {
            if (item.isOn && item != activo)
            {
                                
                activo.isOn = false;
                activo = item;
            }
        }
       
    }

    public void EditarMapa(float x, float z)
    {

        int x_temp = (int)(x / 10);
        int z_temp = (int)(z / 10);

        if ((x_temp < alto && z_temp < ancho) && (x_temp >= 0 && z_temp >= 0))
        {

            switch (activo.tag)
            {
                case ("oro"):
                    InstanciarCubos(x_temp, z_temp, oro);
                    break;
                case ("plata"):
                    InstanciarCubos(x_temp, z_temp, plata);
                    break;
                case ("cobre"):
                    InstanciarCubos(x_temp, z_temp, cobre);
                    break;
                case ("pared"):
                    InstanciarCubos(x_temp, z_temp, pared);
                    break;
                case ("borrar"):
                    EliminarElemento(x_temp, z_temp);
                    break;
                default:
                    break;
            }
        }

    }

    public void InstanciarCubos(int x, int z, GameObject prefab)
    {
        EliminarElemento(x, z);
        GameObject temp = Instantiate(prefab, new Vector3((z * 10) + 5, 0, (x * 10) + 5), Quaternion.identity);
        mapa[x][z] = temp.name;
        mina.transform.parent = temp.transform;

    }
    public void SaveMine()
    {
        Minas mina = GameObject.FindGameObjectWithTag("Minas").GetComponent<Minas>();
        
        mina.mapas.Add(mina.contador,mapa);
        mina.contador++;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }


}
