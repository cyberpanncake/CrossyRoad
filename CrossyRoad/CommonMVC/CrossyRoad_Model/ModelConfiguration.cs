using System;
using System.Collections.Generic;
using System.IO;

namespace CrossyRoad_Model
{
  /// <summary>
  /// Хранилище настроек модели приложения
  /// </summary>
  public static class ModelConfiguration
  {
    #region Общие
    /// <summary>
    /// Абсолютный путь до папки приложения
    /// </summary>
    public static readonly string AppHomeDirectoryPath =
      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Properties.Resources.Game_Name);
    #endregion

    #region Главное меню
    /// <summary>
    /// Позиции пунктов меню
    /// </summary>
    public enum MenuMainItemsPositions : int
    {
      /// <summary>
      /// Позиция пункта "Играть"
      /// </summary>
      Game,
      /// <summary>
      /// Позиция пункта "Рекорды"
      /// </summary>
      Highscores,
      /// <summary>
      /// Позиция пункта "Справка"
      /// </summary>
      Information,
      /// <summary>
      /// Позиция пункта "Выход"
      /// </summary>
      Exit
    }
    /// <summary>
    /// Сопоставление позиции пункта главного меню с его отображаемым именем
    /// </summary>
    private static readonly Dictionary<MenuMainItemsPositions, string> _menuMainItemsNames = new Dictionary<MenuMainItemsPositions, string>()
    {
      { MenuMainItemsPositions.Game, Properties.Resources.Menu_Item_Game },
      { MenuMainItemsPositions.Highscores, Properties.Resources.Menu_Item_Highscores },
      { MenuMainItemsPositions.Information, Properties.Resources.Menu_Item_Information },
      { MenuMainItemsPositions.Exit, Properties.Resources.Menu_Item_Exit }
    };

    /// <summary>
    /// Получает имя пункта главного меню по его позиции
    /// </summary>
    /// <param name="parPosition">Позиция пункта главного меню</param>
    /// <returns>Имя пункта главного меню</returns>
    public static string GetMenuMainItemName(MenuMainItemsPositions parPosition)
    {
      return _menuMainItemsNames[parPosition];
    }
    #endregion

    #region Таблица рекордов
    /// <summary>
    /// Максимальное количество записей в таблице рекордов
    /// </summary>
    public const int MAX_HIGHSCORES_COUNT = 10;
    /// <summary>
    /// Имя файла с таблицей рекордов
    /// </summary>
    public const string HIGHSCORES_FILENAME = "highscores.txt";
    #endregion

    #region Игра
    #region Общие для игры
    /// <summary>
    /// Количество видимых полос препятствий
    /// (при инициализации, когда первая полоса начинается ровно от верхней границы игрового поля)
    /// </summary>
    public const int CELLS_Y_COUNT = 10;
    /// <summary>
    /// Количество видимых клеток полосы препятствий
    /// (при инициализации, когда все полосы начинаются ровно от левой границы игрового поля)
    /// </summary>
    public const int CELLS_X_COUNT = 15;
    /// <summary>
    /// Погрешность измерения вещественных координат
    /// </summary>
    public const float COORDINATE_EPS = 0.001f;
    /// <summary>
    /// Время, через которое происходит обновление состояния игрового поля (мс)
    /// </summary>
    public const int GAME_FIELD_REFRESH_TIME_PERIOD = 20;
    /// <summary>
    /// Количество пустых полос типа Поле в начальном состоянии игрового поля
    /// </summary>
    public const int START_EMPTY_FIELDS_COUNT = 4;
    /// <summary>
    /// Скорость смещения игрового поля вслед за игроком (клетка/сек)
    /// </summary>
    public const float GAME_FIELD_SPEED = 2.5f;
    #endregion

    #region Полоса препятствий
    /// <summary>
    /// Высота полос препятствий (клетка)
    /// </summary>
    public const float LEVEL_HEIGHT = 1;
    /// <summary>
    /// Начальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    public const float LEVEL_START_SPEED = 0.1f;
    /// <summary>
    /// Максимальная скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    public const float LEVEL_MAX_SPEED = 2f;
    /// <summary>
    /// Величина изменения скорости перемещения полосы препятствий (клетка/сек)
    /// </summary>
    public const float LEVEL_SPEED_DELTA = 0.01f;
    /// <summary>
    /// Период времени, через который происходит увеличение скорости полос препятствий (сек)
    /// </summary>
    public const int LEVEL_SPEED_INCREASE_TIME_PERIOD = 10;
    /// <summary>
    /// Максимальное количество идущих подряд полос препятствий одного типа
    /// </summary>
    public const int MAX_LEVELS_SEQUENCE = 4;
    #endregion

    #region Игрок
    /// <summary>
    /// Ширина игрока
    /// </summary>
    public const int PLAYER_WIDTH = 1;
    /// <summary>
    /// Индекс полосы препятствий, на которой стоит игрок в начале игры
    /// </summary>
    public const int PLAYER_START_LEVEL_INDEX = 3;
    /// <summary>
    /// Максимальное расстояние от игрока до нижнего края игрового поля
    /// </summary>
    public const int PLAYER_Y_AND_GAME_FIELD_Y_MIN_DISTANCE = CELLS_Y_COUNT - (PLAYER_START_LEVEL_INDEX + 1);
    /// <summary>
    /// Расстояние от левого края игрового поля до игрока
    /// </summary>
    public const int PLAYER_X_AND_GAME_FIELD_X_DISTANCE = CELLS_X_COUNT / 2;
    #endregion

    #region Поле
    /// <summary>
    /// Вероятность генерации статичного препятствия на клетке поля (от 0 до 1)
    /// </summary>
    public const float BARRIER_GENERATION_PROBABILITY = 0.5f;
    /// <summary>
    /// Ширина статичного препятствия
    /// </summary>
    public const int BARRIER_WIDTH = 1;
    /// <summary>
    /// Максимальное количество идущих подряд статичных препятствий
    /// </summary>
    public const int MAX_BARRIER_SEQUENCE = 3;
    #endregion

    #region Дорога
    /// <summary>
    /// Минимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    public const float MIN_CAR_SPEED = 1.2f;
    /// <summary>
    /// Максимальная скорость перемещения машины по горизонтали (клетка/сек)
    /// </summary>
    public const float MAX_CAR_SPEED = 1.8f;
    /// <summary>
    /// Минимальное расстояние между машинами (клетка)
    /// </summary>
    public const int MIN_DISTANCE_BETWEEN_CARS = 2;
    /// <summary>
    /// Максимальное расстояние между машинами (клетка)
    /// </summary>
    public const int MAX_DISTANCE_BETWEEN_CARS = 5;
    /// <summary>
    /// Ширина машины
    /// </summary>
    public const int CAR_WIDTH = 2;
    #endregion

    #region Река
    /// <summary>
    /// Минимальная скорость перемещения бревна по горизонтали (клетка/сек)
    /// </summary>
    public const float MIN_LOG_SPEED = 0.6f;
    /// <summary>
    /// Максимальная скорость перемещения бревна по горизонтали (клетка/сек)
    /// </summary>
    public const float MAX_LOG_SPEED = 1f;
    /// <summary>
    /// Минимальное расстояние между брёвнами (клетка)
    /// </summary>
    public const int MIN_DISTANCE_BETWEEN_LOGS = 0;
    /// <summary>
    /// Максимальное расстояние между брёвнами (клетка)
    /// </summary>
    public const int MAX_DISTANCE_BETWEEN_LOGS = 3;
    /// <summary>
    /// Минимальная ширина бревна
    /// </summary>
    public const int LOG_WIDTH_MIN = 2;
    /// <summary>
    /// Максимальная ширина бревна
    /// </summary>
    public const int LOG_WIDTH_MAX = 4;
    #endregion
    #endregion

    #region Ввод имени игрока
    /// <summary>
    /// Максимальное количество символов в имени игрока
    /// </summary>
    public const int MAX_PLAYER_NAME_LENGTH = 20;
    #endregion
  }
}