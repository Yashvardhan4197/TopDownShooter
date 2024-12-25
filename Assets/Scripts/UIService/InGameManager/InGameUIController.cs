
public class InGameUIController
{
    private InGameUIView inGameUIView;

    public InGameUIController(InGameUIView inGameUIView)
    {
        this.inGameUIView = inGameUIView;
        this.inGameUIView.SetController(this);
    }

    public void ResumeGame()
    {

    }

    public void RestartGame()
    {

    }

    public void ExitToLobby()
    {

    }
}
