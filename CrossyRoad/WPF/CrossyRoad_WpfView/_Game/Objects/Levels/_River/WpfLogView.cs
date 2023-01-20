using CrossyRoad_Model;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_View._Game.Objects.Levels._River;
using CrossyRoad_WpfView._Game.Objects.Images;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossyRoad_WpfView._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление бревна в приложении WPF
  /// </summary>
  public class WpfLogView : LogView
  {
    /// <summary>
    /// Изображение бревна
    /// </summary>
    public Image Image { get; private set; }

    /// <summary>
    /// Конструктор представления бревна в приложении WPF
    /// </summary>
    /// <param name="parLog">Модель бревна</param>
    public WpfLogView(Log parLog)
      : base(parLog)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Image
        {
          Stretch = Stretch.Uniform,
          FlowDirection = Log.SpeedX > 0 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft
        };
        if (Math.Abs(Log.Width - 2) < ModelConfiguration.COORDINATE_EPS)
        {
          Image.Source = WpfGameObjectsImages.Log2;
        }
        if (Math.Abs(Log.Width - 3) < ModelConfiguration.COORDINATE_EPS)
        {
          Image.Source = WpfGameObjectsImages.Log3;
        }
        if (Math.Abs(Log.Width - 4) < ModelConfiguration.COORDINATE_EPS)
        {
          Image.Source = WpfGameObjectsImages.Log4;
        }
        Panel.SetZIndex(Image, 1);
      });
    }

    /// <summary>
    /// Отображает бревно на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = Log.Width * Scale;
      Image.Height = Log.Height * Scale;
      Canvas.SetLeft(Image, (Log.AbsoluteX - ((Level)Log.Parent).GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Log.AbsoluteY - ((Level)Log.Parent).GameField.AbsoluteY) * Scale);
    }
  }
}