using CrossyRoad_View._GameOver;

namespace CrossyRoad_Controller._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры
  /// </summary>
  public abstract class GameOverController : Controller
  {
    /// <summary>
    /// Набранные очки
    /// </summary>
    protected int Score { get; }

    /// <summary>
    /// Представление окна окончания игры
    /// </summary>
    protected GameOverView GameOverView { get; }

    /// <summary>
    /// Конструктор контроллера окна окончания игры
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parGameOverView">Представление окна окончания игры</param>
    public GameOverController(int parScore, GameOverView parGameOverView)
      : base()
    {
      Score = parScore;
      GameOverView = parGameOverView;
    }
  }
}