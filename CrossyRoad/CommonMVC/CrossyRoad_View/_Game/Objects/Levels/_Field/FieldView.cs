using CrossyRoad_Model._Game.Objects.Levels._Field;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrossyRoad_View._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление поля
  /// </summary>
  public abstract class FieldView : LevelView
  {
    /// <summary>
    /// Модель поля
    /// </summary>
    protected Field Field { get; private set; }
    /// <summary>
    /// Список представлений статичных препятствий
    /// </summary>
    public List<BarrierView> BarriersViews { get; private set; }

    /// <summary>
    /// Конструктор представления поля
    /// </summary>
    /// <param name="parField">Модель поля</param>
    public FieldView(Field parField)
      : base()
    {
      Field = parField;
      BarriersViews = new List<BarrierView>();
      foreach (Barrier elBarrier in Field.Barriers)
      {
        AddBarrierView(elBarrier);
      }
      Field.Barriers.CollectionChanged += BarriersCollectionChanged;
    }

    /// <summary>
    /// Обработчик изменения коллекции статичных препятствий
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Данные события изменения коллекции</param>
    private void BarriersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && (e.NewItems?[0] is Barrier newBarrier))
      {
        AddBarrierView(newBarrier);
      }
    }

    /// <summary>
    /// Добавляет представление статичного препятствия в список
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    protected abstract void AddBarrierView(Barrier parBarrier);
  }
}