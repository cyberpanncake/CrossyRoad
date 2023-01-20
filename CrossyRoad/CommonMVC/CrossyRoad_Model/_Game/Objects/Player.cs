using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._River;

namespace CrossyRoad_Model._Game.Objects
{
  /// <summary>
  /// Модель игрока
  /// </summary>
  public class Player : GameObject
  {
    /// <summary>
    /// Полоса препятствий, на которой находится игрок
    /// </summary>
    public Level CurrentLevel
    {
      get
      {
        return (Level)(Parent is Log log ? log.Parent : Parent);
      }
    }

    /// <summary>
    /// Конструктор модели игрока
    /// </summary>
    /// <param name="parCurrentLevel">Полоса препятствий, на которой находится игрок</param>
    public Player(Level parCurrentLevel)
      : base(parCurrentLevel, ModelConfiguration.CELLS_X_COUNT / 2, 0,
          ModelConfiguration.PLAYER_WIDTH, ModelConfiguration.LEVEL_HEIGHT)
    {
    }
  }
}