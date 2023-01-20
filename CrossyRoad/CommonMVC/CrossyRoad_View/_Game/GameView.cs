using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace CrossyRoad_View._Game
{
  /// <summary>
  /// Представление окна с игровым процессом (пункт меню "Играть")
  /// </summary>
  public abstract class GameView : View
  {
    /// <summary>
    /// Модель игрового процесса
    /// </summary>
    private readonly Game _game;
    /// <summary>
    /// Представление игрового поля
    /// </summary>
    protected GameFieldView GameFieldView { get; }
    /// <summary>
    /// Поток выполнения отображения игрового процесса
    /// </summary>
    private readonly Thread _gameDrawingThread;
    /// <summary>
    /// Промежуток времени, через который происходит перерисовка (мс)
    /// </summary>
    private int _drawingPeriod;

    /// <summary>
    /// Конструктор представления игрового процесса
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public GameView(Game parGame)
      : base()
    {
      _game = parGame;
      GameFieldView = CreateGameFieldView(_game.GameField);
      _gameDrawingThread = new Thread(ProcessDrawing)
      {
        IsBackground = true
      };
      _game.ScoreChanged += DrawScore;
      DrawScore(_game.Score);
    }

    /// <summary>
    /// Создаёт представление игрового поля
    /// </summary>
    /// <param name="parGameField">Модель игрового поле</param>
    /// <returns>Представление игрового поля</returns>
    protected abstract GameFieldView CreateGameFieldView(GameField parGameField);

    /// <summary>
    /// Запускает поток выполнения перерисовки игры через равные промежутки времени
    /// </summary>
    /// <param name="parDrawingPeriod">Промежуток времени, через который происходит перерисовка (мс)</param>
    public void StartDrawing(int parDrawingPeriod)
    {
      _drawingPeriod = parDrawingPeriod;
      _gameDrawingThread.Start();
    }

    /// <summary>
    /// Останавливает поток выполнения перерисовки игры
    /// </summary>
    public void StopDrawing()
    {
      if (_gameDrawingThread.IsAlive)
      {
        _gameDrawingThread.Interrupt();
      }
    }

    /// <summary>
    /// Выполняет перерисовку игры через равные промежутки времени
    /// </summary>
    private void ProcessDrawing()
    {
      try
      {
        DrawScore(_game.Score);
        while (true)
        {
          Thread.Sleep(_drawingPeriod);
          _game.GameSynchronizationEvent.WaitOne();
          Draw();
          _game.GameSynchronizationEvent.Set();
        }
      }
      catch (ThreadInterruptedException)
      {
        _game.GameSynchronizationEvent.Set();
      }
      catch (TaskCanceledException)
      {
        _game.GameSynchronizationEvent.Set();
      }
    }

    /// <summary>
    /// Отображает текущее состояние модели игрового поля на экране приложения
    /// </summary>
    public override void Draw()
    {
      GameFieldView.Draw();
    }

    /// <summary>
    /// Отображает набранные очки на экране приложения
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    protected abstract void DrawScore(int parScore);
  }
}