using CrossyRoad_Model._Game.Objects.Levels._Road;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrossyRoad_View._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление дороги
  /// </summary>
  public abstract class RoadView : LevelView
  {
    /// <summary>
    /// Модель дороги
    /// </summary>
    protected Road Road { get; private set; }
    /// <summary>
    /// Список представлений машин
    /// </summary>
    public List<CarView> CarsViews { get; private set; }

    /// <summary>
    /// Конструктор представления дороги
    /// </summary>
    /// <param name="parRoad">Модель дороги</param>
    public RoadView(Road parRoad)
      : base()
    {
      Road = parRoad;
      CarsViews = new List<CarView>();
      foreach (Car elCar in Road.Cars)
      {
        AddCarView(elCar);
      }
      Road.Cars.CollectionChanged += CarsCollectionChanged;
    }

    /// <summary>
    /// Обработчик изменения коллекции машин
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Данные события изменения коллекции</param>
    private void CarsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && (e.NewItems?[0] is Car newCar))
      {
        AddCarView(newCar);
      }
    }

    /// <summary>
    /// Добавляет представление машины в список
    /// </summary>
    /// <param name="parCar">Модель машины</param>
    protected abstract void AddCarView(Car parCar);

    /// <summary>
    /// Отображает только машины (без поверхности дороги)
    /// </summary>
    public abstract void DrawCars();
  }
}