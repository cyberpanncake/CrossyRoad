using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_View._Game.Objects.Levels._Field;
using CrossyRoad_WpfView._Game.Objects.Images;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossyRoad_WpfView._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление статичного препятствия в приложении WPF
  /// </summary>
  public class WpfBarrierView : BarrierView
  {
    /// <summary>
    /// Изображение статичного препятствия
    /// </summary>
    public Image Image { get; private set; }

    /// <summary>
    /// Конструктор представления статичного препятствия в приложении WPF
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    public WpfBarrierView(Barrier parBarrier)
      : base(parBarrier)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Image
        {
          Source = Type == BarrierTypes.Stone ? WpfGameObjectsImages.Stone : WpfGameObjectsImages.Bush,
          Stretch = Stretch.Uniform
        };
        Panel.SetZIndex(Image, 1);
      });
    }

    /// <summary>
    /// Отображает статичное препятствие на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = Barrier.Width * Scale;
      Image.Height = Barrier.Height * Scale;
      Canvas.SetLeft(Image, (Barrier.AbsoluteX - ((Level)Barrier.Parent).GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Barrier.AbsoluteY - ((Level)Barrier.Parent).GameField.AbsoluteY) * Scale);
    }
  }
}