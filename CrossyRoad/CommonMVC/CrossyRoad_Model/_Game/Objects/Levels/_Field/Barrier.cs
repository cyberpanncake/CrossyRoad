namespace CrossyRoad_Model._Game.Objects.Levels._Field
{
  /// <summary>
  /// Модель статичного препятствия
  /// </summary>
  public class Barrier : GameObject
  {
    /// <summary>
    /// Конструктор модели статичного препятствия
    /// </summary>
    /// <param name="parField">Поле, на котором расположено препятствие</param>
    /// <param name="parX">Координата препятствия по горизонтали (клетка)</param>
    public Barrier(Field parField, float parX)
      : base(parField, parX, 0, ModelConfiguration.BARRIER_WIDTH, ModelConfiguration.LEVEL_HEIGHT)
    {
    }
  }
}