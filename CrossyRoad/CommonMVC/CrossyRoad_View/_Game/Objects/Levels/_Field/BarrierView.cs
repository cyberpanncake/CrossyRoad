using CrossyRoad_Model._Game.Objects.Levels._Field;
using System;

namespace CrossyRoad_View._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление статичного препятствия
  /// </summary>
  public abstract class BarrierView : GameObjectView
  {
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    private static readonly Random _random = new Random();
    /// <summary>
    /// Модель статичного препятствия
    /// </summary>
    protected Barrier Barrier { get; private set; }
    /// <summary>
    /// Тип представления статичного препятствия
    /// </summary>
    protected BarrierTypes Type { get; private set; }

    /// <summary>
    /// Конструктор представления статичного препятствия
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    public BarrierView(Barrier parBarrier)
      : base()
    {
      Barrier = parBarrier;
      Type = (BarrierTypes)_random.Next(Enum.GetNames(typeof(BarrierTypes)).Length);
    }
  }
}