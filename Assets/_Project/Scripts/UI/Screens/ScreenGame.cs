namespace FunnyBlox.GUI
{
  public class ScreenGame : GUICanvasGroup
  {
    public void OnResetButton()
    {
      EventsHandler.ResetWorld();
    }
  }
}