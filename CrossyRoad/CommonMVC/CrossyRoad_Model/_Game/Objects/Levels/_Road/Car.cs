using System;

namespace CrossyRoad_Model._Game.Objects.Levels._Road
{
  /// <summary>
  /// Модель машины
  /// </summary>
  public class Car : MovingGameObject
  {
    /// <summary>
    /// Конструктор модели машины
    /// </summary>
    /// <param name="parRoad">Дорога, по которой едет машина</param>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <param name="parSpeed">Скорость по горизонтали (клетка/сек)</param>
    public Car(Road parRoad, float parX, float parSpeed)
      : base(parRoad, parX, 0, ModelConfiguration.CAR_WIDTH, ModelConfiguration.LEVEL_HEIGHT, parSpeed, 0)
    {
    }
  }
}