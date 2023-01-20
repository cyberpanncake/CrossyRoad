using CrossyRoad_ConsoleView._GameOver;
using CrossyRoad_Controller._GameOver;
using System;

namespace CrossyRoad_ConsoleController._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры в консольном приложении
  /// </summary>
  public class ConsoleGameOverController : GameOverController
  {
    /// <summary>
    /// Конструктор контроллера окна окончания игры в консольном приложении
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public ConsoleGameOverController(int parScore)
      : base(parScore, new ConsoleGameOverView(parScore))
    {
    }

    /// <summary>
    /// Запускает переход в окно окончания игры под управлением контроллера
    /// </summary>
    public override void Start()
    {
      GameOverView.Draw();
      ConsoleControllerUtils.StartStaticWindowKeyboardProccessing(this, ConsoleKey.Escape);
    }
  }
}