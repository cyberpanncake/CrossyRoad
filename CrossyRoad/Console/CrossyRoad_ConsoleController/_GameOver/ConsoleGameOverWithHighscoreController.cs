using CrossyRoad_ConsoleView._GameOver;
using CrossyRoad_Controller._GameOver;
using CrossyRoad_Model._Highscores;
using CrossyRoad_Model._PlayerNameInput;
using System;

namespace CrossyRoad_ConsoleController._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры с новым рекордом в консольном приложении
  /// </summary>
  public class ConsoleGameOverWithHighscoreController : GameOverWithHighscoreController
  {
    /// <summary>
    /// Конструктор контроллера окна окончания игры с новым рекордом в консольном приложении
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    public ConsoleGameOverWithHighscoreController(int parScore, PlayerNameInput parPlayerNameInput)
      : base(parScore, parPlayerNameInput, new ConsoleGameOverWithHighscoreView(parScore, parPlayerNameInput))
    {
      PlayerNameInput.Changed += GameOverWithHighscoreView.RedrawPlayerName;
    }

    /// <summary>
    /// Запускает переход в окно окончания игры с новым рекордом под управлением контроллера
    /// </summary>
    public override void Start()
    {
      GameOverWithHighscoreView.Draw();
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
          case ConsoleKey.Backspace:
            PlayerNameInput.DeleteLastSymbol();
            break;
          case ConsoleKey.Escape:
            return;
          case ConsoleKey.Enter:
            IsWorkEnded = PlayerNameInput.IsValid;
            break;
          default:
            PlayerNameInput.AddSymbol(keyInfo.KeyChar);
            break;
        }
      } while (!IsWorkEnded);
      Highscores.Instance.AddHighscoreAndSaveToFile(new Highscore(PlayerNameInput.Name, Score));
    }
  }
}