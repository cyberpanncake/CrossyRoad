using CrossyRoad_ConsoleView._Game.Objects;
using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game;
using CrossyRoad_View._Game.Objects;
using System;

namespace CrossyRoad_ConsoleView._Game
{
  /// <summary>
  /// Представление игрового процесса в консольном приложении
  /// </summary>
  public class ConsoleGameView : GameView
  {
    /// <summary>
    /// Конструктор представления игрового процесса в консольном приложении
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public ConsoleGameView(Game parGame)
      : base(parGame)
    {
    }

    /// <summary>
    /// Создаёт представление игрового поля
    /// </summary>
    /// <param name="parGameField">Модель игрового поля</param>
    /// <returns>Представление игрового поля</returns>
    protected override GameFieldView CreateGameFieldView(GameField parGameField)
    {
      return new ConsoleGameFieldView(parGameField);
    }

    /// <summary>
    /// Отображает текущее состояние игрового процесса на экране приложения
    /// </summary>
    public override void Draw()
    {
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_Game);
      base.Draw();
    }

    /// <summary>
    /// Отображает набранные очки на экране приложения
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    protected override void DrawScore(int parScore)
    {
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      Console.BackgroundColor = ConsoleViewConfiguration.DEFAULT_BACK_COLOR;
      Console.SetCursorPosition(2, 1);
      Console.Write($"Очки: {parScore}");
    }
  }
}