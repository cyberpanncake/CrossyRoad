using CrossyRoad_Model._Game.Objects;

namespace CrossyRoad_View._Game.Objects
{
  /// <summary>
  /// Представление игрока
  /// </summary>
  public abstract class PlayerView : GameObjectView
  {
    /// <summary>
    /// Модель игрока
    /// </summary>
    protected Player Player { get; private set; }

    /// <summary>
    /// Конструктор представления игрока
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    public PlayerView(Player parPlayer)
      : base()
    {
      Player = parPlayer;
    }
  }
}