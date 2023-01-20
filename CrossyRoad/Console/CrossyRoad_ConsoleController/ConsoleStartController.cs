using CrossyRoad_ConsoleController._Menu;
using CrossyRoad_ConsoleView;
using CrossyRoad_Controller;
using System.Threading;

namespace CrossyRoad_ConsoleController
{
  /// <summary>
  /// Контроллер стартового окна в консольном приложении
  /// </summary>
  public class ConsoleStartController : Controller
  {
    /// <summary>
    /// Запускает стартовое под управлением контроллера
    /// </summary>
    public override void Start()
    {
      ConsoleUtils.PrepareWindow();
      new ConsoleStartView().Draw();
      Thread.Sleep(1000);
      new ConsoleMenuMainController().Start();
    }
  }
}