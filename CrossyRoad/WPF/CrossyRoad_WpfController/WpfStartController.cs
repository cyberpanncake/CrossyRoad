using CrossyRoad_Controller;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using System.Threading;

namespace CrossyRoad_WpfController
{
  /// <summary>
  /// Контроллер стартового окна в приложении WPF
  /// </summary>
  public class WpfStartController : Controller
  {
    /// <summary>
    /// Запускает стартовое окно под управлением контроллера
    /// </summary>
    public override void Start()
    {
      WpfUtils.InitializeAndShowWindow();
      WpfUtils.SetWindowCanResize(false);
      WpfStartView startView = new WpfStartView();
      startView.AnimationEnded += StartMenu;
      startView.Draw();
    }

    /// <summary>
    /// Открывает главное меню
    /// </summary>
    private void StartMenu()
    {
      Thread.Sleep(1000);
      WpfUtils.SetWindowCanResize(true);
      new WpfMenuMainController().Start();
    }
  }
}