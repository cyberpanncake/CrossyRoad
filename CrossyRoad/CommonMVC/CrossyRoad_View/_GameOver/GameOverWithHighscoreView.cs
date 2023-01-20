using CrossyRoad_Model._PlayerNameInput;

namespace CrossyRoad_View._GameOver
{
  /// <summary>
  /// Представление окна окончания игры с новым рекордом (и вводом имени игрока)
  /// </summary>
  public abstract class GameOverWithHighscoreView : GameOverView
  {
    /// <summary>
    /// Модель вводимого имени игрока
    /// </summary>
    protected PlayerNameInput PlayerNameInput { get; }

    /// <summary>
    /// Конструктор представления окна окончания игры с новым рекордом
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    public GameOverWithHighscoreView(int parScore, PlayerNameInput parPlayerNameInput)
      : base(parScore)
    {
      PlayerNameInput = parPlayerNameInput;
    }

    /// <summary>
    /// Отображает текущее введённое имя игрока
    /// </summary>
    public abstract void RedrawPlayerName();
  }
}