using UnityEngine;

namespace FunnyBlox.GUI
{
  public class UIScreensHandler : MonoBehaviour
  {
    public GUICanvasGroup screenGame;
    public GUICanvasGroup screenGameOver;

    private void OnEnable()
    {
      EventsHandler.OnResetWorld += ScreenGameShow;
      EventsHandler.OnGameOver += ScreenGameOverShow;
    }

    private void OnDisable()
    {
      EventsHandler.OnResetWorld -= ScreenGameShow;
      EventsHandler.OnGameOver -= ScreenGameOverShow;
    }

    private void ScreenGameShow()
    {
      screenGameOver.Hide();
            
      screenGame.Show();
    }
        
    private void ScreenGameOverShow()
    {
      screenGame.Hide();
            
      screenGameOver.Show();
    }
  }
}