namespace CrossyRoad_View._GameOver
{
  /// <summary>
  /// Представление окна окончания игры
  /// </summary>
  public abstract class GameOverView : View
  {
    /// <summary>
    /// Набранные очки
    /// </summary>
    protected int Score { get; }

    /// <summary>
    /// Конструктор представления окна окончания игры
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public GameOverView(int parScore)
      : base()
    {
      Score = parScore;
    }
  }
}