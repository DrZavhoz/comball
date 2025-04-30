namespace FunnyBlox.GUI
{
  public class ScreenGameOver : GUICanvasGroup
  {
    public void OnResetButton()
    {
      EventsHandler.ResetWorld();
    }
  }
}