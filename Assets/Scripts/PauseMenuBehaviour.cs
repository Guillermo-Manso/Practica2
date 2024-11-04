using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject hallButtons;

    Screens actualScreen;

    public delegate void PauseDelegate();
    event PauseDelegate PauseAll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterFunctionToPause(ActiveMenu);
        SetScreen(Screens.Game);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(actualScreen == Screens.Game)
            {
                SetScreen(Screens.Hall);
            }
            else
            {
                SetScreen(Screens.Game);
            }
        }
    }

    public void Pause()
    {
        if (PauseAll != null)
        {
            PauseAll();
        }
    }

    void SetScreen(Screens _newScreen)
    {
        actualScreen = _newScreen;

        switch (actualScreen)
        {
            case Screens.Hall:
                ActiveMenu();
                break;
            case Screens.Objects:
                OpenInventory();
                break;
            case Screens.Options:
                break;
            case Screens.Game:
                DeactiveMenu();
                break;
        }
    }

    void ActiveMenu()
    {
        menu.SetActive(true);
        CloseInventory();
    }

    void DeactiveMenu()
    {
        menu.SetActive(false);
    }
    public void OpenInventory()
    {
        inventory.SetActive(true);
        hallButtons.SetActive(false);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        hallButtons.SetActive(true);
    }


    public void RegisterFunctionToPause(PauseDelegate _function)
    {
        PauseAll += _function;
    }

    public void UnRegisterFunctionToPause(PauseDelegate _function)
    {
        PauseAll -= _function;
    }


    public enum Screens
    {
        Hall,
        Objects,
        Options,
        Game
    }
}
