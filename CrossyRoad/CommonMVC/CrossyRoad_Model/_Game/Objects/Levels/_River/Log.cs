using System;

namespace CrossyRoad_Model._Game.Objects.Levels._River
{
  /// <summary>
  /// Модель бревна
  /// </summary>
  public class Log : MovingGameObject
  {
    /// <summary>
    /// Конструктор модели бревна случайной ширины
    /// </summary>
    /// <param name="parRiver">Река, по которой плывёт бревно</param>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <param name="parSpeed">Скорость по горизонтали (клетка/сек)</param>
    public Log(River parRiver, float parX, float parSpeed)
      : base(parRiver, parX, 0,
          Random.Next(ModelConfiguration.LOG_WIDTH_MIN, ModelConfiguration.LOG_WIDTH_MAX + 1),
          ModelConfiguration.LEVEL_HEIGHT, parSpeed, 0)
    {
    }

    /// <summary>
    /// Конструктор модели бревна заданной ширины
    /// </summary>
    /// <param name="parRiver">Река, по которой плывёт бревно</param>
    /// <param name="parX">Координата по горизонтали</param>
    /// <param name="parWidth">Ширина бревна (клетка)</param>
    /// <param name="parSpeed">Скорость по горизонтали (клетка/сек)</param>
    public Log(River parRiver, float parX, int parWidth, float parSpeed)
      : base(parRiver, parX, 0, parWidth, ModelConfiguration.LEVEL_HEIGHT, parSpeed, 0)
    {
    }
  }
}