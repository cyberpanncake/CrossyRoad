using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_View._Game.Objects.Levels._Field;
using CrossyRoad_WpfView._Game.Objects.Images;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CrossyRoad_WpfView._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление поля в приложении WPF
  /// </summary>
  public class WpfFieldView : FieldView
  {
    /// <summary>
    /// Изображение поля
    /// </summary>
    public Rectangle Image { get; private set; }

    /// <summary>
    /// Конструктор представления поля в приложении WPF
    /// </summary>
    /// <param name="parField">Модель поля</param>
    public WpfFieldView(Field parField)
      : base(parField)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Rectangle
        {
          Fill = new ImageBrush
          {
            ImageSource = WpfGameObjectsImages.Field,
            ViewportUnits = BrushMappingMode.Absolute,
            TileMode = TileMode.Tile
          }
        };
        Panel.SetZIndex(Image, 0);
      });
    }

    /// <summary>
    /// Отображает поле на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = (Field.GameField.Width + 2) * Scale;
      Image.Height = Field.Height * Scale;
      ((ImageBrush)Image.Fill).Viewport = new Rect(0, 0, Image.Height, Image.Height);
      Canvas.SetLeft(Image, (Math.Truncate(Field.GameField.AbsoluteX) - 1 - Field.GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Field.AbsoluteY - Field.GameField.AbsoluteY) * Scale);
      foreach (BarrierView elBarrierView in BarriersViews)
      {
        elBarrierView.Draw();
      }
    }

    /// <summary>
    /// Добавляет представление статичного препятствия в список
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    protected override void AddBarrierView(Barrier parBarrier)
    {
      BarriersViews.Add(new WpfBarrierView(parBarrier));
    }
  }
}