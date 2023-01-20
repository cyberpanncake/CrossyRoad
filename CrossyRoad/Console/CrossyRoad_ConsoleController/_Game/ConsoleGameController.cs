using CrossyRoad_ConsoleController._GameOver;
using CrossyRoad_ConsoleView;
using CrossyRoad_ConsoleView._Game;
using CrossyRoad_Controller._Game;
using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Highscores;
using CrossyRoad_Model._PlayerNameInput;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CrossyRoad_ConsoleController._Game
{
  /// <summary>
  /// Контроллер окна с игровым процессом (пункт меню "Играть") в консольном приложении
  /// </summary>
  public class ConsoleGameController : GameController
  {
    /// <summary>
    /// Соответствие нажатых клавиш и направлений движения игрока
    /// </summary>
    private static readonly Dictionary<ConsoleKey, Directions> _keyDirections = new Dictionary<ConsoleKey, Directions>()
    {
      { ConsoleKey.UpArrow, Directions.Up },
      { ConsoleKey.DownArrow, Directions.Down },
      { ConsoleKey.LeftArrow, Directions.Left },
      { ConsoleKey.RightArrow, Directions.Right }
    };
    /// <summary>
    /// Флаг наступления события окончания игры
    /// </summary>
    private bool _gameOver;
    /// <summary>
    /// Флаг необходимости остановки игрового процесса
    /// </summary>
    private bool _needToStopGame;
    /// <summary>
    /// Поток обработки нажатий клавиш пользователем
    /// </summary>
    private readonly Thread _processingKeyboardThread;

    /// <summary>
    /// Конструктор контроллера окна с игровым процессом в консольном приложении
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public ConsoleGameController(Game parGame)
      : base(parGame, new ConsoleGameView(parGame))
    {
      _gameOver = false;
      _needToStopGame = false;
      Game.GameOver += () => { _gameOver = true; };
      Game.GameOver += StopGame;
      _processingKeyboardThread = new Thread(ProcessKeyInput);
    }

    /// <summary>
    /// Выполняет процесс обработки нажатых пользователем клавиш во время игрового процесса
    /// </summary>
    private void ProcessKeyInput()
    {
      try
      {
        while (Console.KeyAvailable)
        {
          Console.ReadKey(true);
        }
        while (!_needToStopGame)
        {
          ConsoleKey key = Console.ReadKey(true).Key;
          if (key == ConsoleKey.Escape)
          {
            StopGame();
            return;
          }
          while (Console.KeyAvailable)
          {
            Console.ReadKey(true);
          }
          if (_keyDirections.ContainsKey(key))
          {
            Game.MovePlayer(_keyDirections[key]);
          }
          Thread.Sleep(300);
        }
      }
      catch (ThreadInterruptedException)
      {
      }
    }

    /// <summary>
    /// Запускает переход в пункт меню "Играть" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      GameView.Draw();
      int y = (int)(ConsoleViewConfiguration.WINDOW_HEIGHT - 3);
      ConsoleUtils.DrawCountDown(3, y, ConsoleColor.White, ConsoleColor.Black);
      Game.Start();
      GameView.StartDrawing(ConsoleViewConfiguration.GAME_REDRAW_PERIOD);
      _processingKeyboardThread.Start();
      while (!_needToStopGame)
      {
      }
      Thread.Sleep(100);
      if (_gameOver)
      {
        GameView.Draw();
        Thread.Sleep(1000);
        if (Highscores.Instance.IsNewHighscore(Game.Score))
        {
          new ConsoleGameOverWithHighscoreController(Game.Score, new PlayerNameInput()).Start();
        }
        else
        {
          new ConsoleGameOverController(Game.Score).Start();
        }
      }
    }

    /// <summary>
    /// Останавливает игровой процесс
    /// </summary>
    private void StopGame()
    {
      if (_processingKeyboardThread.IsAlive)
      {
        _processingKeyboardThread.Interrupt();
      }
      Game.Stop();
      GameView.StopDrawing();
      _needToStopGame = true;
    }
  }
}