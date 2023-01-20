using CrossyRoad_Model;
using CrossyRoad_Model._Highscores;
using CrossyRoad_View._Highscores;
using System;

namespace CrossyRoad_ConsoleView._Highscores
{
  /// <summary>
  /// Представление окна с таблицей рекордов (пункт меню "Рекорды") в консольном приложении 
  /// </summary>
  public class ConsoleHighscoresView : HighscoresView
  {
    /// <summary>
    /// Ширина колонки имён игроков
    /// </summary>
    private int _columnPlayerWidth;
    /// <summary>
    /// Ширина колонки набранных очков
    /// </summary>
    private int _columnScoreWidth;

    /// <summary>
    /// Конструктор представления окна с таблицей рекордов в консольном приложении
    /// </summary>
    /// <param name="parHighscores">Модель таблицы рекордов</param>
    public ConsoleHighscoresView(Highscores parHighscores)
      : base(parHighscores)
    {
      SetHighscoresTablePositionAndSize();
    }

    /// <summary>
    /// Рассчитывает и устанавливает положение и размеры таблицы рекордов
    /// </summary>
    private void SetHighscoresTablePositionAndSize()
    {
      int maxColumnPlayerWidth = CrossyRoad_View.Properties.Resources.Highscores_Column_Player.Length;
      int maxColumnScoreWidth = CrossyRoad_View.Properties.Resources.Highscores_Column_Score.Length;
      foreach (Highscore highscore in Highscores.Scores)
      {
        if (highscore.Player.Length > maxColumnPlayerWidth)
        {
          maxColumnPlayerWidth = highscore.Player.Length;
        }
        if (highscore.Score.ToString().Length > maxColumnScoreWidth)
        {
          maxColumnScoreWidth = highscore.Score.ToString().Length;
        }
      }
      _columnPlayerWidth = maxColumnPlayerWidth + 2;
      _columnScoreWidth = maxColumnScoreWidth + 2;
      Width = maxColumnPlayerWidth + maxColumnScoreWidth + 3;
      Height = ModelConfiguration.MAX_HIGHSCORES_COUNT * 2 + 3;
      ConsoleUtils.GetViewPositionInCenterWithHelpText(Width, Height, out float y, out float x);
      X = x;
      Y = y;
    }

    /// <summary>
    /// Отображает окно с таблицей рекордов на экране приложения
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      Console.BackgroundColor = ConsoleViewConfiguration.DEFAULT_BACK_COLOR;
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Highscores_Title, 3);
      DrawTableBorder();
      int x = (int)X + 2;
      int y = (int)Y + 1;
      Console.SetCursorPosition(x, y);
      Console.Write(CrossyRoad_View.Properties.Resources.Highscores_Column_Player);
      Console.SetCursorPosition(x + _columnPlayerWidth + 1, y);
      Console.Write(CrossyRoad_View.Properties.Resources.Highscores_Column_Score);
      y += 2;
      for (int i = 0; i < Highscores.Scores.Length; i++)
      {
        Console.SetCursorPosition(x, y + i * 2);
        Console.Write(Highscores.Scores[i].Player);
        Console.SetCursorPosition(x + _columnPlayerWidth + 1, y + i * 2);
        Console.Write(Highscores.Scores[i].Score);
      }
    }

    /// <summary>
    /// Отображает рамки таблицы
    /// </summary>
    private void DrawTableBorder()
    {
      int x = (int)X;
      int y = (int)Y;
      Console.SetCursorPosition(x, y);
      // Верхняя граница таблицы
      Console.Write("╔");
      for (int i = 0; i < _columnPlayerWidth; i++)
      {
        Console.Write("═");
      }
      Console.Write("╦");
      for (int i = 0; i < _columnScoreWidth; i++)
      {
        Console.Write("═");
      }
      Console.Write("╗");
      // Промежуточные и боковые границы таблицы
      for (int i = 0; i < ModelConfiguration.MAX_HIGHSCORES_COUNT + 1; i++)
      {
        Console.SetCursorPosition(x, ++y);
        Console.Write("║");
        Console.SetCursorPosition(x + _columnPlayerWidth + 1, y);
        Console.Write("║");
        Console.SetCursorPosition(x + _columnPlayerWidth + _columnScoreWidth + 2, y);
        Console.Write("║");
        if (i < ModelConfiguration.MAX_HIGHSCORES_COUNT)
        {
          Console.SetCursorPosition(x, ++y);
          Console.Write("╟");
          for (int j = 0; j < _columnPlayerWidth; j++)
          {
            Console.Write("─");
          }
          Console.Write("╫");
          for (int j = 0; j < _columnScoreWidth; j++)
          {
            Console.Write("─");
          }
          Console.Write("╢");
        }
      }
      // Нижняя граница таблицы
      Console.SetCursorPosition(x, ++y);
      Console.Write("╚");
      for (int i = 0; i < _columnPlayerWidth; i++)
      {
        Console.Write("═");
      }
      Console.Write("╩");
      for (int i = 0; i < _columnScoreWidth; i++)
      {
        Console.Write("═");
      }
      Console.Write("╝");
    }
  }
}