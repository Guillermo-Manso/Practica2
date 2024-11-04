using UnityEngine;
using UnityEngine.UI;

public class InventoryBehaviour : MonoBehaviour
{
    [SerializeField] Transform buttonPos;

    [SerializeField] ItemProperties[] objects;
    [SerializeField] ItemProperties[] inventoryButtons;

    [SerializeField] ScrollRect scrollRect;

    [SerializeField] int objectType;
    int count;
    int actualObject;

    private void Start()
    {
        inventoryButtons = new ItemProperties[objects.Length];
        scrollRect.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewObject(objectType , 1);
        }
    }

    /// <summary>
    /// Metodo en el que se crea un nuevo objeto
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_amount"></param>
    public void NewObject(int _type, int _amount)
    {
        //Se llama al metodo para comprobar si ya existia y asi no volver a crearlo
        bool _exist = Comprobation(objects[_type]);

        if (!_exist)
        {
            //Se instancia en la escena
            ItemProperties _newObject = Instantiate(objects[_type]);
            
            //Se anade a la lista de objetos que tiene el invetario mediante codigo
            inventoryButtons[count] = _newObject;


            //Se suma la cantidad recogida
            AddAmount(_amount, inventoryButtons[count]);

            //Se aumenta la cuenta para el proximo objeto recogido
            count++;

            //Se hace hijo del inventario para que se coloque en su sitio
            _newObject.transform.SetParent(buttonPos, true);
        }
        else
        {
            //Si existe el objeto se le sumara la cantidad recogida simplemente
            AddAmount(_amount, inventoryButtons[actualObject]);
        }
    }

    /// <summary>
    /// Metodo en el que se comprueba si el objeto existe
    /// </summary>
    /// <param name="_newObject"></param>
    /// <returns></returns>
    bool Comprobation(ItemProperties _newObject)
    {
        //Se comprueba uno a uno si el id del objeto ya estaba en el inventario ademas de si es un espacio vacio
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            if (inventoryButtons[i] != null && _newObject.IdItem == inventoryButtons[i].IdItem)
            {
                actualObject = i;
                return true;
            }
        }
        //En caso de no coindicir se determina que no existia el objeto en la escena
        return false;
    }

    /// <summary>
    /// Metodo en el que se suma la cantidad recogida al texto del menu
    /// </summary>
    /// <param name="_amount"></param>
    /// <param name="_object"></param>
    void AddAmount(int _amount, ItemProperties _object)
    {
        //Hecho asi de forma temporal
        _object.amount += _amount;
        _object.amountText.text = _object.amount.ToString();
    }
    /*
    public void OpenInventory()
    {
        inventory.SetActive(true);
        hallButtons.SetActive(false);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        hallButtons.SetActive(true);
    }*/
}
