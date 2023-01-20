using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects.Levels._Road;
using CrossyRoad_WpfView._Game.Objects.Images;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossyRoad_WpfView._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление машины в приложении WPF
  /// </summary>
  public class WpfCarView : CarView
  {
    /// <summary>
    /// Изображение машины
    /// </summary>
    public Image Image { get; private set; }

    /// <summary>
    /// Конструктор представления машины в консольном приложении
    /// </summary>
    /// <param name="parCar">Модель машины</param>
    public WpfCarView(Car parCar)
      : base(parCar)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Image
        {
          Source = Color switch
          {
            CarColors.Blue => WpfGameObjectsImages.CarBlue,
            CarColors.Green => WpfGameObjectsImages.CarGreen,
            CarColors.Red => WpfGameObjectsImages.CarRed,
            CarColors.Purple => WpfGameObjectsImages.CarPurple,
            _ => null
          },
          Stretch = Stretch.Uniform,
          FlowDirection = Car.SpeedX > 0 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft
        };
        Panel.SetZIndex(Image, 2);
      });
    }

    /// <summary>
    /// Отображает машину на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = Car.Width * Scale;
      Image.Height = Car.Height * Scale;
      Canvas.SetLeft(Image, (Car.AbsoluteX - ((Level)Car.Parent).GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Car.AbsoluteY - ((Level)Car.Parent).GameField.AbsoluteY) * Scale);
    }
  }
}