using CrossyRoad_View._GameOver;
using System;

namespace CrossyRoad_ConsoleView._GameOver
{
  /// <summary>
  /// Представление окна окончания игры в консольном приложении
  /// </summary>
  public class ConsoleGameOverView : GameOverView
  {
    /// <summary>
    /// Конструктор представления окна окончания игры в консольном приложении
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public ConsoleGameOverView(int parScore)
      : base(parScore)
    {
      Height = 3;
      ConsoleUtils.GetViewPositionInCenterWithHelpText(0, Height, out float y, out float x);
      Y = y;
    }

    /// <summary>
    /// Отображает окно окончания игры на экране приложения
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.GameOver_GameOver, (int)Y);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.GameOver_Score + Score, (int)Y + 2);
    }
  }
}