using CrossyRoad_Model._Game.Objects.Levels._River;

namespace CrossyRoad_View._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление бревна
  /// </summary>
  public abstract class LogView : GameObjectView
  {
    /// <summary>
    /// Модель бревна
    /// </summary>
    protected Log Log { get; private set; }

    /// <summary>
    /// Конструктор представления бревна
    /// </summary>
    /// <param name="parLog">Модель бревна</param>
    public LogView(Log parLog)
      : base()
    {
      Log = parLog;
    }
  }
}