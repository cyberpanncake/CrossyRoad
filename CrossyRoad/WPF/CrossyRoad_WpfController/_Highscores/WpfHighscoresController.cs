using CrossyRoad_Controller._Highscores;
using CrossyRoad_Model._Highscores;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._Highscores;
using System.Windows.Input;

namespace CrossyRoad_WpfController._Highscores
{
  /// <summary>
  /// Контроллер окна с таблицей рекордов (пункт меню "Рекорды") в приложении WPF
  /// </summary>
  public class WpfHighscoresController : HighscoresController
  {
    /// <summary>
    /// Конструктор контроллера окна с таблицей рекордов в приложении WPF
    /// </summary>
    public WpfHighscoresController()
      : base(Highscores.Instance, new WpfHighscoresView())
    {
    }

    /// <summary>
    /// Запускает переход в пункт меню "Рекорды" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
      HighscoresView.Draw();
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