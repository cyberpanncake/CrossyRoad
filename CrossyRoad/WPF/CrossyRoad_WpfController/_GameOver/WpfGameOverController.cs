using CrossyRoad_Controller._GameOver;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._GameOver;
using System;
using System.Windows.Input;

namespace CrossyRoad_WpfController._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры в приложении WPF
  /// </summary>
  public class WpfGameOverController : GameOverController
  {
    /// <summary>
    /// Конструктор контроллера окна окончания игры в приложении WPF
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public WpfGameOverController(int parScore)
      : base(parScore, new WpfGameOverView(parScore))
    {
    }

    /// <summary>
    /// Запускает переход в окно окончания игры под управлением контроллера
    /// </summary>
    public override void Start()
    {
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
      GameOverView.Draw();
    }

    /// <summary>
    /// Обработчик события нажатия клавиши
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="args">Аргументы события нажатия клавиши</param>
    public void KeyDownEventHandler(object sender, KeyEventArgs args)
    {
      if (args.Key == Key.Escape)
      {
        WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
        new WpfMenuMainController().Start();
      }
    }
  }
}