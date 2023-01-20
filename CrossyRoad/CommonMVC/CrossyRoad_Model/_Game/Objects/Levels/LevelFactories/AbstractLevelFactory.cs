using System;
using System.Collections.ObjectModel;

namespace CrossyRoad_Model._Game.Objects.Levels.LevelFactories
{
  /// <summary>
  /// Абстрактная фабрика создания полос препятствий
  /// </summary>
  public abstract class AbstractLevelFactory
  {
    /// <summary>
    /// Игровое поле
    /// </summary>
    protected GameField GameField { get; }
    /// <summary>
    /// Существующие полосы препятствий
    /// </summary>
    protected ObservableCollection<Level> ExistingLevels { get; }
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    protected static readonly Random Random = new Random();

    /// <summary>
    /// Конструктор фабрики полос препятствий
    /// </summary>
    /// <param name="parGameField">Игровое поле</param>
    public AbstractLevelFactory(GameField parGameField)
    {
      GameField = parGameField;
      ExistingLevels = parGameField.Levels;
    }

    /// <summary>
    /// Создаёт полосу препятствий одного из возможных типов в зависимости от уже существующих полос
    /// </summary>
    /// <returns>Полоса препятствий</returns>
    public abstract Level Create();
  }
}