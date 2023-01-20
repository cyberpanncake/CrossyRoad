using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Game.Objects.Levels;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CrossyRoad_Model._Game
{
  /// <summary>
  /// Модель игрового процесса
  /// </summary>
  public class Game
  {
    /// <summary>
    /// Делегат окончания игры
    /// </summary>
    public delegate void dGameOver();
    /// <summary>
    /// Событие окончания игры
    /// </summary>
    public event dGameOver GameOver = null;
    /// <summary>
    /// Делегат обновления очков
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public delegate void dScoreChanged(int parScore);
    /// <summary>
    /// Событие обновления очков
    /// </summary>
    public event dScoreChanged ScoreChanged = null;
    /// <summary>
    /// Флаг окончания игры
    /// </summary>
    private bool _gameOver = false;
    /// <summary>
    /// Игровое поле
    /// </summary>
    public GameField GameField { get; }
    /// <summary>
    /// Набранные очки
    /// </summary>
    public int Score { get; private set; }
    /// <summary>
    /// Поток выполнения игрового процесса
    /// </summary>
    private readonly Thread _gameThread;
    /// <summary>
    /// Часы, измеряющие время с последнего перемещения
    /// </summary>
    private readonly Stopwatch _changeStateStopwatch;
    /// <summary>
    /// Часы, измеряющие время с последнего увеличения скорости перемещения полос препятствий
    /// </summary>
    private readonly Stopwatch _levelsSpeedIncreaseWatch;
    /// <summary>
    /// Событие синхронизации потоков записи/чтения игрового поля и набранных очков
    /// </summary>
    public AutoResetEvent GameSynchronizationEvent { get; }

    /// <summary>
    /// Конструктор модели игрового процесса
    /// </summary>
    public Game()
    {
      GameField = new GameField();
      Score = 0;
      _gameThread = new Thread(ProccessGame)
      {
        IsBackground = true
      };
      _changeStateStopwatch = new Stopwatch();
      _levelsSpeedIncreaseWatch = new Stopwatch();
      GameSynchronizationEvent = new AutoResetEvent(true);
    }

    /// <summary>
    /// Запускает поток выполнения игрового процесса
    /// </summary>
    public void Start()
    {
      _gameThread.Start();
    }

    /// <summary>
    /// Останавливает поток выполнения игрового процесса
    /// </summary>
    public void Stop()
    {
      if (_gameThread.IsAlive)
      {
        _gameThread.Interrupt();
      }
    }

    /// <summary>
    /// Выполняет игровой процесс
    /// </summary>
    private void ProccessGame()
    {
      try
      {
        _changeStateStopwatch.Restart();
        _levelsSpeedIncreaseWatch.Restart();
        while (!_gameOver)
        {
          Thread.Sleep(ModelConfiguration.GAME_FIELD_REFRESH_TIME_PERIOD);
          GameSynchronizationEvent.WaitOne();
          if (_gameOver)
          {
            break;
          }
          ChangeGameFieldState();
          IncreaseLevelsSpeedIfNeed();
         _gameOver = IsGameOver();
          GameSynchronizationEvent.Set();
        }
        GameOver?.Invoke();
      }
      catch (ThreadInterruptedException)
      {
        GameSynchronizationEvent.Set();
      }
      catch (TaskCanceledException)
      {
        GameSynchronizationEvent.Set();
      }
    }

    /// <summary>
    /// Изменяет состояние модели игрового поля в соответствии с прошедшим временем
    /// </summary>
    private void ChangeGameFieldState()
    {
      _changeStateStopwatch.Stop();
      long elapsedMilliseconds = _changeStateStopwatch.ElapsedMilliseconds;
      _changeStateStopwatch.Restart();
      GameField.ChangeState(elapsedMilliseconds);
    }

    /// <summary>
    /// Увеличивает скорость полос препятствий, если прошло достаточное количество времени
    /// </summary>
    private void IncreaseLevelsSpeedIfNeed()
    {
      _levelsSpeedIncreaseWatch.Stop();
      long elapsedMilliseconds = _levelsSpeedIncreaseWatch.ElapsedMilliseconds / 1000;
      if (elapsedMilliseconds >= GameSettings.LevelSpeedIncreaseTimePeriod)
      {
        _levelsSpeedIncreaseWatch.Restart();
        GameField.IncreaseLevelsSpeed();
      }
      else
      {
        _levelsSpeedIncreaseWatch.Start();
      }
    }

    /// <summary>
    /// Проверяет, произошла ли ситуация окончания игры
    /// </summary>
    /// <returns>true - если игра окончена, false - в противном случае</returns>
    private bool IsGameOver()
    {
      Player player = GameField.Player;
      // если игрок попал в ситуацию окончания игры на полосе
      if (player.CurrentLevel.IsGameOverOnX(player.AbsoluteX))
      {
        return true;
      }
      // если игрок достиг нижнего края игрового поля
      if (player.AbsoluteY + player.Height * 0.5 > GameField.Y + GameField.Height)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Перемещает игрока на клетку в одном направлении
    /// </summary>
    /// <param name="parDirection">Направление движения игрока</param>
    public void MovePlayer(Directions parDirection)
    {
      try
      {
        GameSynchronizationEvent.WaitOne();
        GameField.MovePlayerOn(parDirection);
        CheckAndSetScore();
        _gameOver = IsGameOver();
        GameSynchronizationEvent.Set();
      }
      catch (ThreadInterruptedException)
      {
        GameSynchronizationEvent.Set();
      }
    }

    /// <summary>
    /// Проверяет достиг ли игрок непосещённой полосы, и если да, то увеличивает количество очков
    /// </summary>
    private void CheckAndSetScore()
    {
      Level currentLevel = GameField.Player.CurrentLevel;
      if (!currentLevel.IsVisited)
      {
        currentLevel.SetLevelVisited();
        Score++;
        ScoreChanged?.Invoke(Score);
      }
    }
  }
}