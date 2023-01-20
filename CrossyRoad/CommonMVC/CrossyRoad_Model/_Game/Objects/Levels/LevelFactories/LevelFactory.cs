using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using System;

namespace CrossyRoad_Model._Game.Objects.Levels.LevelFactories
{
  /// <summary>
  /// Фабрика создания полос препятствий
  /// </summary>
  public class LevelFactory : AbstractLevelFactory
  {
    /// <summary>
    /// Конструктор фабрики полос препятствий
    /// </summary>
    /// <param name="parGameField">Игровое поле</param>
    public LevelFactory(GameField parGameField) : base(parGameField)
    {
    }

    /// <summary>
    /// Создаёт полосу препятствий одного из возможных типов в зависимости от уже существующих полос
    /// </summary>
    /// <returns>Полоса препятствий</returns>
    public override Level Create()
    {
      if (ExistingLevels.Count < ModelConfiguration.START_EMPTY_FIELDS_COUNT)
      {
        Field emptyField;
        if (ExistingLevels.Count == 0)
        {
          emptyField = new Field(GameField, ModelConfiguration.CELLS_Y_COUNT - 1, null, true);
        }
        else
        {
          emptyField = new Field(GameField, ExistingLevels[^1].Y - 1, ExistingLevels[^1], true);
        }
        emptyField.SetLevelVisited();
        return emptyField;
      }
      LevelTypes type;
      int n = Enum.GetNames(typeof(LevelTypes)).Length;
      do
      {
        type = (LevelTypes)Random.Next(n);
      } while (!CanCreate(type));
      return CreateLevel(type);
    }

    /// <summary>
    /// Проверяет, можно ли создать полосу препятствий выбранного типа
    /// </summary>
    /// <param name="parType">Тип создаваемой полосы препятствий</param>
    /// <returns>true - если можно создать полосу данного типа, false - в противном случае</returns>
    private bool CanCreate(LevelTypes parType)
    {
      bool canCreate = false;
      for (int i = 1; i <= GameSettings.MaxLevelsSequence; i++)
      {
        switch (parType)
        {
          case LevelTypes.Field:
            canCreate |= !(ExistingLevels[^i] is Field);
            break;
          case LevelTypes.Road:
            canCreate |= !(ExistingLevels[^i] is Road);
            break;
          case LevelTypes.River:
            canCreate |= !(ExistingLevels[^i] is River);
            break;
        }
      }
      return canCreate;
    }

    /// <summary>
    /// Создаёт полосу препятствий выбранного типа
    /// </summary>
    /// <param name="parType">Тип полосы препятствий</param>
    /// <returns>Новая полоса препятствий</returns>
    private Level CreateLevel(LevelTypes parType)
    {
      float y = ExistingLevels[^1].Y - 1;
      return parType switch
      {
        LevelTypes.Field => new Field(GameField, y, ExistingLevels[^1]),
        LevelTypes.Road => new Road(GameField, y),
        LevelTypes.River => new River(GameField, y),
        _ => null,
      };
    }
  }
}