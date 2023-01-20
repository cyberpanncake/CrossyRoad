using CrossyRoad_ConsoleView._Highscores;
using CrossyRoad_Controller._Highscores;
using CrossyRoad_Model._Highscores;
using System;

namespace CrossyRoad_ConsoleController._Highscores
{
  /// <summary>
  /// Контроллер окна с таблицей рекордов (пункт меню "Играть") в консольном приложении
  /// </summary>
  public class ConsoleHighscoresController : HighscoresController
  {
    /// <summary>
    /// Конструктор контроллера окна с таблицей рекордов в консольном приложении
    /// </summary>
    /// <param name="parHighscores">Модель таблицы рекордов</param>
    public ConsoleHighscoresController(Highscores parHighscores)
      : base(parHighscores, new ConsoleHighscoresView(parHighscores))
    {
    }

    /// <summary>
    /// Запускает переход в пункт меню "Рекорды" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      HighscoresView.Draw();
      ConsoleControllerUtils.StartStaticWindowKeyboardProccessing(this, ConsoleKey.Escape);
    }
  }
}