using System;

namespace CrossyRoad_Model._Game
{
  /// <summary>
  /// Настройки игры
  /// </summary>
  public static class GameSettings
  {
    #region Полосы препятствий
    /// <summary>
    /// Максимальное количество идущих подряд полос препятствий одного типа
    /// </summary>
    private static int? _maxLevelsSequence = null;
    /// <summary>
    /// Максимальное количество идущих подряд полос препятствий одного типа
    /// </summary>
    public static int MaxLevelsSequence
    {
      get
      {
        if (_maxLevelsSequence == null)
        {
          return ModelConfiguration.MAX_LEVELS_SEQUENCE;
        }
        return (int)_maxLevelsSequence;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальное количество идущих подряд полос препятствий должно быть больше 0!");
        }
        _maxLevelsSequence = value;
      }
    }
    /// <summary>
    /// Начальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    private static float? _levelStartSpeed = null;
    /// <summary>
    /// Начальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    public static float LevelStartSpeed
    {
      get
      {
        if (_levelStartSpeed == null)
        {
          return ModelConfiguration.LEVEL_START_SPEED;
        }
        return (float)_levelStartSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Начальная скорость перемещения полос препятствий должна быть больше 0!");
        }
        if (value > LevelMaxSpeed)
        {
          throw new ArgumentException("Начальная скорость перемещения полос препятствий не должна быть больше максимальной!");
        }
        _levelStartSpeed = value;
      }
    }
    /// <summary>
    /// Начальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    private static float? _levelsMaxSpeed = null;
    /// <summary>
    /// Начальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    public static float LevelMaxSpeed
    {
      get
      {
        if (_levelsMaxSpeed == null)
        {
          return ModelConfiguration.LEVEL_MAX_SPEED;
        }
        return (float)_levelsMaxSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальная скорость перемещения полос препятствий должна быть больше 0!");
        }
        if (value < LevelStartSpeed)
        {
          throw new ArgumentException("Максимальная скорость перемещения полос препятствий не должна быть меньше начальной!");
        }
        _levelsMaxSpeed = value;
      }
    }
    /// <summary>
    /// Период времени, через который происходит увеличение скорости полос препятствий (мс)
    /// </summary>
    private static int? _levelSpeedIncreaseTimePeriod = null;
    public static int LevelSpeedIncreaseTimePeriod
    {
      get
      {
        if (_levelSpeedIncreaseTimePeriod == null)
        {
          return ModelConfiguration.LEVEL_SPEED_INCREASE_TIME_PERIOD;
        }
        return (int)_levelSpeedIncreaseTimePeriod;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Период времени увеличения скорости полос препятствий должен быть больше 0!");
        }
        _levelSpeedIncreaseTimePeriod = value;
      }
    }
    #endregion

    #region Поле
    /// <summary>
    /// Вероятность генерации статичного препятствия на клетке поля (от 0 до 1)
    /// </summary>
    private static float? _barrierGenerationProbability = null;
    /// <summary>
    /// Вероятность генерации статичного препятствия на клетке поля (от 0 до 1)
    /// </summary>
    public static float BarrierGenerationProbability
    {
      get
      {
        if (_barrierGenerationProbability == null)
        {
          return ModelConfiguration.BARRIER_GENERATION_PROBABILITY;
        }
        return (float)_barrierGenerationProbability;
      }
      set
      {
        if (value < 0 || value > 1)
        {
          throw new ArgumentException("Вероятность генерации статичного препятствия должна быть в диапазоне от 0 до 1!");
        }
        _barrierGenerationProbability = value;
      }
    }
    #endregion Поле

    #region Дорога
    /// <summary>
    /// Минимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    private static float? _minCarSpeed = null;
    /// <summary>
    /// Минимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    public static float MinCarSpeed
    {
      get
      {
        if (_minCarSpeed == null)
        {
          return ModelConfiguration.MIN_CAR_SPEED;
        }
        return (float)_minCarSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Минимальная скорость машины должна быть больше 0!");
        }
        if (value > MaxCarSpeed)
        {
          throw new ArgumentException("Минимальная скорость машины не должна быть больше максимальной!");
        }
        _minCarSpeed = value;
      }
    }
    /// <summary>
    /// Максимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    private static float? _maxCarSpeed = null;
    /// <summary>
    /// Максимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    public static float MaxCarSpeed
    {
      get
      {
        if (_maxCarSpeed == null)
        {
          return ModelConfiguration.MAX_CAR_SPEED;
        }
        return (float)_maxCarSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальная скорость машины должна быть больше 0!");
        }
        if (value < MinCarSpeed)
        {
          throw new ArgumentException("Максимальная скорость машины не должна быть больше максимальной!");
        }
        _maxCarSpeed = value;
      }
    }
    /// <summary>
    /// Минимальное расстояние между машинами (клетка)
    /// </summary>
    private static int? _minDistanceBetweenCars = null;
    /// <summary>
    /// Минимальное расстояние между машинами (клетка)
    /// </summary>
    public static int MinDistanceBetweenCars
    {
      get
      {
        if (_minDistanceBetweenCars == null)
        {
          return ModelConfiguration.MIN_DISTANCE_BETWEEN_CARS;
        }
        return (int)_minDistanceBetweenCars;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Минимальное расстояние между машинами должно быть больше 0!");
        }
        if (value > MaxDistanceBetweenCars)
        {
          throw new ArgumentException("Минимальное расстояние между машинами не должно быть больше максимального!");
        }
        _minDistanceBetweenCars = value;
      }
    }
    /// <summary>
    /// Максимальное расстояние между машинами (клетка)
    /// </summary>
    private static int? _maxDistanceBetweenCars = null;
    /// <summary>
    /// Максимальное расстояние между машинами (клетка)
    /// </summary>
    public static int MaxDistanceBetweenCars
    {
      get
      {
        if (_maxDistanceBetweenCars == null)
        {
          return ModelConfiguration.MAX_DISTANCE_BETWEEN_CARS;
        }
        return (int)_maxDistanceBetweenCars;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальное расстояние между машинами должно быть больше 0!");
        }
        if (value < MinDistanceBetweenCars)
        {
          throw new ArgumentException("Максимальное расстояние между машинами не должно быть меньше минимального!");
        }
        _maxDistanceBetweenCars = value;
      }
    }
    #endregion

    #region Река
    /// <summary>
    /// Минимальная скорость перемещения бревна по горизонтали (клетка/сек)
    /// </summary>
    private static float? _minLogSpeed = null;
    /// <summary>
    /// Минимальная скорость перемещения бревна по горизонтали (клетка/сек)
    /// </summary>
    public static float MinLogSpeed
    {
      get
      {
        if (_minLogSpeed == null)
        {
          return ModelConfiguration.MIN_LOG_SPEED;
        }
        return (float)_minLogSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Минимальная скорость бревна должна быть больше 0!");
        }
        if (value > MaxLogSpeed)
        {
          throw new ArgumentException("Минимальная скорость бревна не должна быть больше максимальной!");
        }
        _minLogSpeed = value;
      }
    }
    /// <summary>
    /// Максимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    private static float? _maxLogSpeed = null;
    /// <summary>
    /// Максимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    public static float MaxLogSpeed
    {
      get
      {
        if (_maxLogSpeed == null)
        {
          return ModelConfiguration.MAX_LOG_SPEED;
        }
        return (float)_maxLogSpeed;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальная скорость бревна должна быть больше 0!");
        }
        if (value < MinLogSpeed)
        {
          throw new ArgumentException("Максимальная скорость бревна не должна быть больше максимальной!");
        }
        _maxLogSpeed = value;
      }
    }
    /// <summary>
    /// Минимальное расстояние между брёвнами (клетка)
    /// </summary>
    private static int? _minDistanceBetweenLogs = null;
    /// <summary>
    /// Минимальное расстояние между брёвнами (клетка)
    /// </summary>
    public static int MinDistanceBetweenLogs
    {
      get
      {
        if (_minDistanceBetweenLogs == null)
        {
          return ModelConfiguration.MIN_DISTANCE_BETWEEN_LOGS;
        }
        return (int)_minDistanceBetweenLogs;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Минимальное расстояние между брёвнами должно быть больше 0!");
        }
        if (value > MaxDistanceBetweenLogs)
        {
          throw new ArgumentException("Минимальное расстояние между брёвнами не должно быть меньше максимального!");
        }
        _minDistanceBetweenLogs = value;
      }
    }
    /// <summary>
    /// Максимальное расстояние между брёвнами (клетка)
    /// </summary>
    private static int? _maxDistanceBetweenLogs = null;
    /// <summary>
    /// Максимальное расстояние между брёвнами (клетка)
    /// </summary>
    public static int MaxDistanceBetweenLogs
    {
      get
      {
        if (_maxDistanceBetweenLogs == null)
        {
          return ModelConfiguration.MAX_DISTANCE_BETWEEN_LOGS;
        }
        return (int)_maxDistanceBetweenLogs;
      }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Максимальное расстояние между брёвнами должно быть больше 0!");
        }
        if (value < MinDistanceBetweenLogs)
        {
          throw new ArgumentException("Максимальное расстояние между брёвнами не должно быть больше минимального!");
        }
        _maxDistanceBetweenLogs = value;
      }
    }
    #endregion
  }
}