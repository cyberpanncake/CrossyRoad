using CrossyRoad_Model._Game.Objects.Levels._Road;
using System;

namespace CrossyRoad_View._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление машины
  /// </summary>
  public abstract class CarView : GameObjectView
  {
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    private static readonly Random _random = new Random();
    /// <summary>
    /// Модель машины
    /// </summary>
    protected Car Car { get; private set; }
    /// <summary>
    /// Цвет машины
    /// </summary>
    protected CarColors Color { get; private set; }

    /// <summary>
    /// Конструктор представления машины
    /// </summary>
    /// <param name="parCar">Модель машины</param>
    public CarView(Car parCar)
      : base()
    {
      Car = parCar;
      Color = (CarColors)_random.Next(Enum.GetNames(typeof(CarColors)).Length);
    }
  }
}