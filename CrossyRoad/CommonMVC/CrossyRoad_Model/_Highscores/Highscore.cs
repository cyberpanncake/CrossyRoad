using System;

namespace CrossyRoad_Model._Highscores
{
  /// <summary>
  /// Модель записи таблицы рекордов
  /// </summary>
  [Serializable]
  public class Highscore
  {
    /// <summary>
    /// Имя игрока
    /// </summary>
    public string Player { get; }
    /// <summary>
    /// Количество набранных очков
    /// </summary>
    public int Score { get; }

    /// <summary>
    /// Конструктор модели записи таблицы рекордов
    /// </summary>
    /// <param name="parPlayer">Имя игрока</param>
    /// <param name="parScore">Количество набранных очков</param>
    public Highscore(string parPlayer, int parScore)
    {
      Player = parPlayer;
      Score = parScore;
    }
  }
}