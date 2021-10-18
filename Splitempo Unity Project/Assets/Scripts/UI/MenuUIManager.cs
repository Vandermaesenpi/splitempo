using UnityEngine;

public class MenuUIManager : MenuUI
{
    [SerializeField] private MenuUI currentUI;
    private void Start() {
        currentUI.Show();
    }

    public void GoToMenu(MenuUI menu){
        if(currentUI != null){currentUI.Hide();}
        menu.Show();
        currentUI = menu;
    }    
    
}
