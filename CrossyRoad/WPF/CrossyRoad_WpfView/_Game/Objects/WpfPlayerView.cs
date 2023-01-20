using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game.Objects;
using CrossyRoad_WpfView._Game.Objects.Images;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Game.Objects
{
  /// <summary>
  /// Представление игрока в приложении WPF
  /// </summary>
  public class WpfPlayerView : PlayerView
  {
    /// <summary>
    /// Изображение игрока
    /// </summary>
    public Image Image { get; private set; }

    /// <summary>
    /// Конструктор представления игрока в приложении WPF
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    public WpfPlayerView(Player parPlayer)
      : base(parPlayer)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Image = new Image
        {
          Source = WpfGameObjectsImages.Player,
          Stretch = System.Windows.Media.Stretch.Uniform
        };
        Panel.SetZIndex(Image, 2);
      });
    }

    /// <summary>
    /// Отображает игрока на экране приложения
    /// </summary>
    public override void Draw()
    {
      Image.Width = Player.Width * Scale;
      Image.Height = Player.Height * Scale;
      Canvas.SetLeft(Image, (Player.AbsoluteX - Player.CurrentLevel.GameField.AbsoluteX) * Scale);
      Canvas.SetTop(Image, (Player.AbsoluteY - 0.5 * Player.Height - Player.CurrentLevel.GameField.AbsoluteY) * Scale);
    }
  }
}