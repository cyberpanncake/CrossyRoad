using CrossyRoad_Model;
using CrossyRoad_Model._PlayerNameInput;
using CrossyRoad_View._GameOver;
using System;

namespace CrossyRoad_ConsoleView._GameOver
{
  /// <summary>
  /// Представление окна окончания игры с новым рекордом в консольном приложении
  /// </summary>
  public class ConsoleGameOverWithHighscoreView : GameOverWithHighscoreView
  {
    /// <summary>
    /// Конструктор представления окна окончания игры с новым рекордом в консольном приложении
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    public ConsoleGameOverWithHighscoreView(int parScore, PlayerNameInput parPlayerNameInput)
      : base(parScore, parPlayerNameInput)
    {
      Height = 9;
      ConsoleUtils.GetViewPositionInCenterWithHelpText(0, Height, out float y, out _);
      Y = y;
    }

    /// <summary>
    /// Отображает окно окончания игры с новым рекордом на экране приложения
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_GameOver_Highscore);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.GameOver_Highscore, (int)Y);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.GameOver_Score + Score, (int)Y + 2);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.GameOver_Highscore_EnterName, (int)Y + 4);
      RedrawPlayerName();
    }

    /// <summary>
    /// Отображает текущее введённое имя игрока
    /// </summary>
    public override void RedrawPlayerName()
    {
      ConsoleUtils.DrawCenteredTextField(ModelConfiguration.MAX_PLAYER_NAME_LENGTH, (int)(Y + 6));
      ConsoleUtils.DrawCenteredText(PlayerNameInput.Name, (int)(Y + 6));
      if (PlayerNameInput.IsValid)
      {
        int messageLength = CrossyRoad_View.Properties.Resources.GameOver_Highscore_InvalidName.Length;
        ConsoleUtils.DrawCenteredTextWithDefaultColors(new string(' ', messageLength), (int)(Y + 8));
      }
      else
      {
        string message = CrossyRoad_View.Properties.Resources.GameOver_Highscore_InvalidName;
        ConsoleUtils.DrawCenteredTextWithDefaultColors(message, (int)(Y + 8));
      }
    }
  }
}