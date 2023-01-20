using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game.Objects;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects
{
  /// <summary>
  /// Представление игрока в консольном приложении
  /// </summary>
  public class ConsolePlayerView : PlayerView
  {
    /// <summary>
    /// Изображение игрока
    /// </summary>
    private readonly ConsoleGameObjectImage _image;

    /// <summary>
    /// Конструктор представления игрока в консольном приложении
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    public ConsolePlayerView(Player parPlayer)
      : base(parPlayer)
    {
      _image = ConsoleGameObjectsImages.Player;
    }

    /// <summary>
    /// Отображает игрока на экране приложения
    /// </summary>
    public override void Draw()
    {
      GameField gameField = Player.CurrentLevel.GameField;
      int i = (int)Math.Round((Player.AbsoluteY - gameField.AbsoluteY - Player.Height / 2) * Scale);
      int j = (int)Math.Round((Player.AbsoluteX - gameField.AbsoluteX) * Scale);
      ConsoleGameObjectsUtils.DrawImageInBuffer(_image, i, j);
    }
  }
}