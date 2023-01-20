using CrossyRoad_Controller._Game;
using CrossyRoad_Model;
using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Highscores;
using CrossyRoad_Model._PlayerNameInput;
using CrossyRoad_WpfController._GameOver;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CrossyRoad_WpfController._Game
{
  /// <summary>
  /// Контроллер окна с игровым процессом (пункт меню "Играть") в приложении WPF
  /// </summary>
  public class WpfGameController : GameController
  {
    /// <summary>
    /// Соответствие нажатых клавиш и направлений движения игрока
    /// </summary>
    private static readonly Dictionary<Key, Directions> _keyDirections = new Dictionary<Key, Directions>()
    {
      { Key.Up, Directions.Up },
      { Key.Down, Directions.Down },
      { Key.Left, Directions.Left },
      { Key.Right, Directions.Right }
    };
    /// <summary>
    /// Время с последнего перемещения игрока
    /// </summary>
    private Stopwatch _playerMotionWatch;

    /// <summary>
    /// Конструктор контроллера окна с игровым процессом в приложении WPF
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public WpfGameController(Game parGame)
      : base(parGame, new WpfGameView(parGame))
    {
      _playerMotionWatch = new Stopwatch();
    }

    /// <summary>
    /// Запускает переход в пункт меню "Играть" под управлением контроллера
    /// </summary>
    public override void Start()
    {
      _playerMotionWatch.Restart();
      Game.GameOver += StopGame;
      ((WpfGameView)GameView).CountDownEnded += StartGame;
      WpfUtils.AddSizeChangedHandlerToWindow(RedrawGameField);
      ((WpfGameView)GameView).DrawWithCountDown();
    }

    /// <summary>
    /// Перерисовывает игровое поле при изменении размеров окна
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Аргументы события изменения размеров окна</param>
    private void RedrawGameField(object sender, SizeChangedEventArgs e)
    {
      GameView.GameFieldView.Draw();
    }

    /// <summary>
    /// Запускает игровой процесс
    /// </summary>
    private void StartGame()
    {
      WpfUtils.RemoveSizeChangedHandlerToWindow(RedrawGameField);
      Game.Start();
      GameView.StartDrawing(WpfViewConfiguration.GAME_REDRAW_PERIOD);
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
    }

    /// <summary>
    /// Обрабатывает событие окончания игры
    /// </summary>
    private void StopGame()
    {
      GameView.StopDrawing();
      GameView.Draw();
      Thread.Sleep(1000);
      WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
      if (Highscores.Instance.IsNewHighscore(Game.Score))
      {
        new WpfGameOverWithHighscoreController(Game.Score, new PlayerNameInput()).Start();
      }
      else
      {
        new WpfGameOverController(Game.Score).Start();
      }
    }

    /// <summary>
    /// Обработчик события нажатия клавиши
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="args">Аргументы события нажатия клавиши</param>
    public void KeyDownEventHandler(object sender, KeyEventArgs args)
    {
      if (args.Key == Key.Escape)
      {
        Game.Stop();
        GameView.StopDrawing();
        Thread.Sleep(100);
        WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
        new WpfMenuMainController().Start();
      }
      if (_keyDirections.ContainsKey(args.Key))
      {
        _playerMotionWatch.Stop();
        long elapsedMilliseconds = _playerMotionWatch.ElapsedMilliseconds;
        if (elapsedMilliseconds >= ModelConfiguration.MIN_TIME_BETWEEN_PLAYER_MOTIONS)
        {
          _playerMotionWatch.Restart();
          Game.MovePlayer(_keyDirections[args.Key]);
        }
        else
        {
          _playerMotionWatch.Start();
        }
      }
    }
  }
}