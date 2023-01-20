using CrossyRoad_ConsoleView._Information;
using CrossyRoad_Controller._Information;
using System;

namespace CrossyRoad_ConsoleController._Information
{
  /// <summary>
  /// Контроллер окна справки (пункт меню "Как играть") в консольном приложении
  /// </summary>
  public class ConsoleInformationController : InformationController
  {
    /// <summary>
    /// Конструктор контроллера окна справки в консольном приложении
    /// </summary>
    public ConsoleInformationController()
      : base(new ConsoleInformationView())
    {
    }

    /// <summary>
    /// Запускает переход в пункт меню "Как играть" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      InformationView.Draw();
      ConsoleControllerUtils.StartStaticWindowKeyboardProccessing(this, ConsoleKey.Escape);
    }
  }
}