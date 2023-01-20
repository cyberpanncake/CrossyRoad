using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_View._Game.Objects.Levels._River;
using CrossyRoad_WpfView._Game.Objects.Images;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CrossyRoad_WpfView._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление реки в консольном приложении
  /// </summary>
  public class WpfRiverView : RiverView
  {
    /// <summary>
    /// Изображение реки
    /// </summary>
    public Rectangle Image { get; private set; }

    /// <summary>
    /// Конструктор представления реки в консольном приложении
    /// </summary>
    /// <param name="parRiver">Модель реки</param>
    public WpfRiverView(River parRiver)
      : base(parRiver)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Rectangle
        {
          Fill = new ImageBrush
          {
            ImageSource = WpfGameObjectsImages.River,
            ViewportUnits = BrushMappingMode.Absolute,
            TileMode = TileMode.Tile
          }
        };
        Panel.SetZIndex(Image, 0);
      });
    }

    /// <summary>
    /// Отображает реку на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = (River.GameField.Width + 2) * Scale;
      Image.Height = River.Height * Scale;
      ((ImageBrush)Image.Fill).Viewport = new Rect(0, 0, Image.Height, Image.Height);
      Canvas.SetLeft(Image, (Math.Truncate(River.GameField.AbsoluteX) - 1 - River.GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (River.AbsoluteY - River.GameField.AbsoluteY) * Scale);
      foreach (LogView elLogView in LogsViews)
      {
        elLogView.Draw();
      }
    }

    /// <summary>
    /// Добавляет представление бревна в список
    /// </summary>
    /// <param name="parLog">Модель бревна</param>
    protected override void AddLogView(Log parLog)
    {
      LogsViews.Add(new WpfLogView(parLog));
    }
  }
}