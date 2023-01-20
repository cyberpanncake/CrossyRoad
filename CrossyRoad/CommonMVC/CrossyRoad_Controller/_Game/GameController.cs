using CrossyRoad_Model._Game;
using CrossyRoad_View._Game;

namespace CrossyRoad_Controller._Game
{
  /// <summary>
  /// Контроллер окна с игровым процессом (пункт меню "Играть")
  /// </summary>
  public abstract class GameController : Controller
  {
    /// <summary>
    /// Модель игрового процесса
    /// </summary>
    protected Game Game { get; }
    /// <summary>
    /// Представление игрового процесса
    /// </summary>
    protected GameView GameView { get; }

    /// <summary>
    /// Конструктор контроллера окна с игровым процессом (пункт меню "Играть")
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    /// <param name="parGameView">Представление игрового процесса</param>
    public GameController(Game parGame, GameView parGameView)
      : base()
    {
      Game = parGame;
      GameView = parGameView;
    }
  }
}