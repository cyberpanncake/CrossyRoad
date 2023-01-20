using CrossyRoad_View;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CrossyRoad_WpfView
{
  /// <summary>
  /// Представление стартового окна в приложении WPF
  /// </summary>
  public class WpfStartView : View
  {
    /// <summary>
    /// Делегат окончания анимации
    /// </summary>
    public delegate void dAnimationEnded();
    /// <summary>
    /// Событие окончания анимации
    /// </summary>
    public event dAnimationEnded AnimationEnded = null;
    /// <summary>
    /// Прямоугольник, заслоняющий логотип и постепенно открывающий его в процессе анимации
    /// </summary>
    private Rectangle _rectangle;

    /// <summary>
    /// Отображает стартовое окно на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.DrawOnWindow(CreateElements());
      AnimateLogo();
    }

    /// <summary>
    /// Создаёт иерархию графических элементов для анимации логотипа
    /// </summary>
    /// <returns>Контейнер логотипа</returns>
    private Grid CreateElements()
    {
      Grid grid = new Grid()
      {
        HorizontalAlignment = HorizontalAlignment.Center
      };
      grid.RowDefinitions.Add(new RowDefinition());
      grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
      grid.RowDefinitions.Add(new RowDefinition());
      TextBlock logo = new TextBlock
      {
        Text = Properties.Resources.Logo,
        FontFamily = WpfViewConfiguration.LogoFontFamily,
        FontSize = WpfViewConfiguration.LOGO_FONT_SIZE
      };
      Grid.SetRow(logo, 1);
      _ = grid.Children.Add(logo);
      Canvas canvas = new Canvas();
      _ = grid.Children.Add(canvas);
      Grid.SetRowSpan(canvas, 3);
      _rectangle = new Rectangle
      {
        Width = 400,
        Height = 100,
        Fill = WpfViewConfiguration.DefaultBackgroundColor
      };
      Canvas.SetLeft(_rectangle, 0);
      Canvas.SetTop(_rectangle, 280);
      _ = canvas.Children.Add(_rectangle);
      return grid;
    }

    /// <summary>
    /// Отображает анимацию логотипа игры
    /// </summary>
    private async void AnimateLogo()
    {
      await Task.Run(() =>
      {
        for (int i = 0; i < 60; i++)
        {
          Thread.Sleep(30);
          WpfUtils.Dispatcher.Invoke(() =>
          {
            Canvas.SetTop(_rectangle, 280 + i);
          });
        }
        AnimationEnded?.Invoke();
      });
    }
  }
}