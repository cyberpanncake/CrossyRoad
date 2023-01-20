namespace CrossyRoad_View._Game.Objects
{
  /// <summary>
  /// Представление игрового объекта
  /// </summary>
  public abstract class GameObjectView : View
  {
    /// <summary>
    /// Коэффициент масштабирования игрового объекта
    /// </summary>
    public static float Scale { get; set; } = 1;

    /// <summary>
    /// Конструктор представления игрового объекта
    /// </summary>
    public GameObjectView()
      : base()
    {
    }
  }
}