
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        SceneUI = Managers.UI.ShowSceneUI<UI_Lobby>(Literals.LOBBY_Scene_Main);

        return true;
    }
}
