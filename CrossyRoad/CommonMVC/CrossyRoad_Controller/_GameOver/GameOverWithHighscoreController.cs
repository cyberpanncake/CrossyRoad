using CrossyRoad_Model._PlayerNameInput;
using CrossyRoad_View._GameOver;

namespace CrossyRoad_Controller._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры с установленным новым рекордом
  /// </summary>
  public abstract class GameOverWithHighscoreController : Controller
  {
    /// <summary>
    /// Набранные очки
    /// </summary>
    protected int Score { get; }
    /// <summary>
    /// Модель вводимого имени игрока
    /// </summary>
    protected PlayerNameInput PlayerNameInput { get; }
    /// <summary>
    /// Представление окна окончания игры с новым рекордом
    /// </summary>
    protected GameOverWithHighscoreView GameOverWithHighscoreView { get; }

    /// <summary>
    /// Конструктор контроллера окна окончания игры с установленным новым рекордом
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    /// <param name="parGameOverWithHighscoreView">Представление окна окончания игры с новым рекордом</param>
    public GameOverWithHighscoreController(int parScore, PlayerNameInput parPlayerNameInput,
      GameOverWithHighscoreView parGameOverWithHighscoreView)
      : base()
    {
      Score = parScore;
      PlayerNameInput = parPlayerNameInput;
      GameOverWithHighscoreView = parGameOverWithHighscoreView;
    }
  }
}