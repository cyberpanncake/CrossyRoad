using CrossyRoad_Controller._Information;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._Information;
using System.Windows.Input;

namespace CrossyRoad_WpfController._Information
{
  /// <summary>
  /// Контроллер окна справки (пункт меню "Как играть") в приложении WPF
  /// </summary>
  public class WpfInformationController : InformationController
  {
    /// <summary>
    /// Конструктор контроллера окна справки в приложении WPF
    /// </summary>
    public WpfInformationController()
      : base(new WpfInformationView())
    {
    }

    /// <summary>
    /// Запускает переход в пункт меню "Как играть" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
      InformationView.Draw();
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