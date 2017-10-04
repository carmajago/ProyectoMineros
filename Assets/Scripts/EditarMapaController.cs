using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 @Param pared,oro,plata,cobre: son los prefabs de los objetos que se van a graficar.SIEMPRE DEBEN TENER TAG
 @param plano: es la superficie donde se encuentra  la escala x y z es el número de filas y columnas del diccionario
 @param mina el Gameobject que contiene todos los prefabs
 @param toggles: lista de toggles que indican que elemento se esta editando.
 @param ancho,alto tamaño del diccionario
 @param activo:toggle auxiliar que indica que elemento se esta editando.
 @param mapa:diccionario que almacena el mapa,este es el que se exporta a json
 @param objetos_mapa aqui se guardan la instancia a los objetos del mapa.
 
 @procedure Start:Constructor.
 @procedure lateUpdate:se llama 1/4 de veces por frame
 @procedure update:se llama frame por frame
 @procedure CrearTerreno:Inicializa todos los gameobjects en la escena
 @procedure EliminarCubo(int x,int z):Elimina un cubo de la escena y de los diccionarios mapa y objetos_mapa
 @procedure ValidarToggles:Valida que solo exista un toggle activo.
 @procedure EditarMapa(float x,float z):Se encarga de controlar cuando se cambia y se elimina un objetodel mapa ,x y z
                                        son las cordenadas del mouse.
 @procedure InstanciarCubos(int x, int z, GameObject prefab):instancia un prefab en el espacio con las cordenadas x,z del
                                                             diccionario
 @procedure SaveMine:Envia el diccionario mapa a un cotrolador "Mina" que se encarga de guardar todas las minas.
     * */
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

    public Dictionary<int, Dictionary<int, GameObject>> objetos_mapa;



    void Start()
    {
        mapa = new Dictionary<int, Dictionary<int, string>>();
        objetos_mapa = new Dictionary<int, Dictionary<int, GameObject>>();
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



    public void CrearTerreno()
    {
        for (int i = 0; i < alto; i++)
        {
            Dictionary<int, string> aux = new Dictionary<int, string>();
            Dictionary<int, GameObject> aux2 = new Dictionary<int, GameObject>();
            for (int j = 0; j < ancho; j++)
            {

                GameObject temp = Instantiate(pared, new Vector3((j * 10) + 5, 0, (i * 10) + 5), Quaternion.identity);
                aux.Add(j, temp.tag);

                temp.transform.parent = mina.transform;
                aux2.Add(j, temp);
            }
            //////Corregir
            mapa.Add(i, aux);
            objetos_mapa.Add(i, aux2);
        }
    }

    public void EliminarCubo(int x, int z)
    {
        Destroy(objetos_mapa[x][z]);
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
                    EliminarCubo(x_temp, z_temp);
                    break;
                default:
                    break;
            }
        }

    }

    public void InstanciarCubos(int x, int z, GameObject prefab)
    {
        EliminarCubo(x, z);
        GameObject temp = Instantiate(prefab, new Vector3((z * 10) + 5, 0, (x * 10) + 5), Quaternion.identity);
        mapa[x][z] = temp.tag;
        mina.transform.parent = temp.transform;
        objetos_mapa[x][z] = temp;

    }
    public void SaveMine()
    {
        Minas mina = GameObject.FindGameObjectWithTag("Minas").GetComponent<Minas>();

        mina.mapas.Add(mina.contador, mapa);
        mina.contador++;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }


}
