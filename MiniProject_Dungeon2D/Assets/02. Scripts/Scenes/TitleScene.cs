
public class TitleScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        SceneUI = Managers.UI.ShowSceneUI<UI_Title>(Literals.TITLE_Scene_Main);

        return true;
    }
}
