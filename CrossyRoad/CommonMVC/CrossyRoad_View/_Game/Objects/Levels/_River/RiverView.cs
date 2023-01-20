using CrossyRoad_Model._Game.Objects.Levels._River;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrossyRoad_View._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление реки
  /// </summary>
  public abstract class RiverView : LevelView
  {
    /// <summary>
    /// Модель реки
    /// </summary>
    protected River River { get; private set; }
    /// <summary>
    /// Список представлений брёвен
    /// </summary>
    public List<LogView> LogsViews { get; private set; }

    /// <summary>
    /// Конструктор представления реки
    /// </summary>
    /// <param name="parRiver">Модель реки</param>
    public RiverView(River parRiver)
      : base()
    {
      River = parRiver;
      LogsViews = new List<LogView>();
      foreach (Log elLog in River.Logs)
      {
        AddLogView(elLog);
      }
      River.Logs.CollectionChanged += LogsCollectionChanged;
    }

    /// <summary>
    /// Обработчик изменения коллекции брёвен
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Данные события изменения коллекции</param>
    private void LogsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && (e.NewItems?[0] is Log newLog))
      {
        AddLogView(newLog);
      }
    }

    /// <summary>
    /// Добавляет представление бревна в список
    /// </summary>
    /// <param name="parLog">Модель бревна</param>
    protected abstract void AddLogView(Log parLog);
  }
}