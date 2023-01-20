using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects.Levels._Road;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление дороги в консольном приложении
  /// </summary>
  public class ConsoleRoadView : RoadView
  {
    /// <summary>
    /// Конструктор представления дороги в консольном приложении
    /// </summary>
    /// <param name="parRoad">Модель дороги</param>
    public ConsoleRoadView(Road parRoad)
      : base(parRoad)
    {
    }

    /// <summary>
    /// Отображает дорогу на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((Road.AbsoluteY - Road.GameField.AbsoluteY) * Scale);
      ConsoleGameObjectsUtils.DrawLevelBackgroundInBuffer(ConsoleColor.DarkGray, i);
      DrawCars();
    }

    /// <summary>
    /// Отображает только машины (без поверхности дороги)
    /// </summary>
    public override void DrawCars()
    {
      foreach (CarView elCarView in CarsViews)
      {
        elCarView.Draw();
      }
    }

    /// <summary>
    /// Добавляет представление машины в список
    /// </summary>
    /// <param name="parCar">Машина</param>
    protected override void AddCarView(Car parCar)
    {
      CarsViews.Add(new ConsoleCarView(parCar));
    }
  }
}