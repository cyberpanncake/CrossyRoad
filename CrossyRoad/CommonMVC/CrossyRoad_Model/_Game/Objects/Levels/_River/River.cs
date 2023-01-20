using System;
using System.Collections.ObjectModel;

namespace CrossyRoad_Model._Game.Objects.Levels._River
{
  /// <summary>
  /// Модель реки
  /// </summary>
  public class River : Level
  {
    /// <summary>
    /// Брёвна
    /// </summary>
    public ObservableCollection<Log> Logs { get; }
    /// <summary>
    /// Скорость всех брёвен на этой реке по горизонтали (клетка/сек)
    /// </summary>
    private readonly float _allLogsSpeed;

    /// <summary>
    /// Конструктор модели реки
    /// </summary>
    /// <param name="parGameField">Игровое поле</param>
    /// <param name="parY">Координата по вертикали (клетка)</param>
    public River(GameField parGameField, float parY)
      : base(parGameField, parY)
    {
      _allLogsSpeed = (float)((Random.Next(2) == 0 ? -1 : 1)
        * (Random.NextDouble() * (GameSettings.MaxLogSpeed - GameSettings.MinLogSpeed) + GameSettings.MinLogSpeed));
      Logs = new ObservableCollection<Log>();
      while (Logs.Count == 0 || Logs[^1].AbsoluteX < GameField.AbsoluteX + GameField.Width)
      {
        Log log = (Log)GenerateObject(true);
        if (log != null)
        {
          Logs.Add(log);
        }
      }
      while (Logs[0].AbsoluteX > GameField.AbsoluteX)
      {
        Log log = (Log)GenerateObject(false);
        if (log != null)
        {
          Logs.Insert(0, log);
        }
      }
    }

    /// <summary>
    /// Генерирует бревно в реке на некотором расстояниии от крайнего
    /// </summary>
    /// <param name="parGenerateOnRight">Флаг генерации бревна справа от уже существующих (true), или слева (false)</param>
    /// <returns>Сгенерированное бревно</returns>
    protected override GameObject GenerateObject(bool parGenerateOnRight)
    {
      float x = GameField.AbsoluteX;
      int dxMin = Logs.Count == 0 ? 0 : GameSettings.MinDistanceBetweenLogs;
      int dx = Random.Next(dxMin, GameSettings.MaxDistanceBetweenLogs + 1);
      if (parGenerateOnRight)
      {
        if (Logs.Count > 0)
        {
          x = Logs[^1].AbsoluteX + Logs[^1].Width;
        }
        dx *= parGenerateOnRight ? 1 : -1;
        return new Log(this, x + dx, _allLogsSpeed);
      }
      else
      {
        int width = Random.Next(ModelConfiguration.LOG_WIDTH_MIN, ModelConfiguration.LOG_WIDTH_MAX + 1);
        if (Logs.Count > 0)
        {
          x = Logs[0].AbsoluteX - width;
        }
        dx *= parGenerateOnRight ? 1 : -1;
        return new Log(this, x + dx, width, _allLogsSpeed);
      }
    }

    /// <summary>
    /// Изменяет состояние модели реки
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public override void ChangeState(long parTimeMilliseconds)
    {
      Move(parTimeMilliseconds);
      foreach (Log elLog in Logs)
      {
        elLog.Move(parTimeMilliseconds);
      }
    }

    /// <summary>
    /// Генерирует бревно в реке, если это необходимо
    /// (например, когда игрок переместился вправо/влево)
    /// </summary>
    public override void GenerateObjectIfNeed()
    {
      if (Logs[0].AbsoluteX > GameField.AbsoluteX)
      {
        Log log = (Log)GenerateObject(false);
        if (log != null)
        {
          Logs.Insert(0, log);
        }
      }
      if (Logs[^1].AbsoluteX + Logs[^1].Width < GameField.AbsoluteX + GameField.Width)
      {
        Log log = (Log)GenerateObject(true);
        if (log != null)
        {
          Logs.Add(log);
        }
      }
    }

    /// <summary>
    /// Проверяет, возникает ли ситуация конца игры на координате по горизонтали
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>true - если игра заканчивается на выбранной координате, false - в противном случае</returns>
    public override bool IsGameOverOnX(float parX)
    {
      foreach (Log elLog in Logs)
      {
        if (elLog.AbsoluteX > parX + ModelConfiguration.PLAYER_WIDTH * 0.5)
        {
          break;
        }
        if (elLog.AbsoluteX + elLog.Width > parX + ModelConfiguration.PLAYER_WIDTH * 0.5)
        {
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Получает бревно, на которое можно переместиться, находясь на координате X, если такое есть
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>Бревно, на которое можно переместиться, а если такого нет - null</returns>
    public Log GetLogToMoveOnX(float parX)
    {
      foreach (Log elLog in Logs)
      {
        if (elLog.AbsoluteX > parX + ModelConfiguration.PLAYER_WIDTH * 0.75)
        {
          break;
        }
        if (elLog.AbsoluteX + elLog.Width > parX + ModelConfiguration.PLAYER_WIDTH * 0.25)
        {
          return elLog;
        }
      }
      return null;
    }
  }
}