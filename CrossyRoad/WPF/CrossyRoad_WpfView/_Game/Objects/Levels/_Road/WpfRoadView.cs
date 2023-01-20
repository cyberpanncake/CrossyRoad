using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects.Levels._Road;
using CrossyRoad_WpfView._Game.Objects.Images;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CrossyRoad_WpfView._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление дороги в приложении WPF
  /// </summary>
  public class WpfRoadView : RoadView
  {
    /// <summary>
    /// Изображение дороги
    /// </summary>
    public Rectangle Image { get; private set; }

    /// <summary>
    /// Конструктор представления дороги в приложении WPF
    /// </summary>
    /// <param name="parRoad">Модель дороги</param>
    public WpfRoadView(Road parRoad)
      : base(parRoad)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Rectangle
        {
          Fill = new ImageBrush
          {
            ImageSource = WpfGameObjectsImages.Road,
            ViewportUnits = BrushMappingMode.Absolute,
            TileMode = TileMode.Tile
          }
        };
        Panel.SetZIndex(Image, 0);
      });
    }

    /// <summary>
    /// Отображает дорогу на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = (Road.GameField.Width + 2) * Scale;
      Image.Height = Road.Height * Scale;
      ((ImageBrush)Image.Fill).Viewport = new Rect(0, 0, Image.Height, Image.Height);
      Canvas.SetLeft(Image, (Math.Truncate(Road.GameField.AbsoluteX) - 1 - Road.GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Road.AbsoluteY - Road.GameField.AbsoluteY) * Scale);
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
    /// <param name="parCar">Модель машины</param>
    protected override void AddCarView(Car parCar)
    {
      CarsViews.Add(new WpfCarView(parCar));
    }
  }
}