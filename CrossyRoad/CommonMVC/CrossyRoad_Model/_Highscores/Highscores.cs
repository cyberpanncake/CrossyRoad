using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CrossyRoad_Model._Highscores
{
  /// <summary>
  /// Модель таблицы рекордов
  /// </summary>
  public class Highscores
  {
    /// <summary>
    /// Единственный объект таблицы рекордов
    /// </summary>
    private static Highscores _instance;

    /// <summary>
    /// Единственный объект таблицы рекордов
    /// </summary>
    public static Highscores Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new Highscores();
        }
        return _instance;
      }
    }
    /// <summary>
    /// Максимальное количество записей в таблице рекордов
    /// </summary>
    private const int MaxHighscoresCount = ModelConfiguration.MAX_HIGHSCORES_COUNT;
    /// <summary>
    /// Имя файла с таблицей рекордов с абсолютным путём до него
    /// </summary>
    private readonly string HighscoresFilename = ModelConfiguration.AppHomeDirectoryPath + "/" + ModelConfiguration.HIGHSCORES_FILENAME;
    /// <summary>
    /// Зафиксированные рекорды
    /// </summary>
    private readonly List<Highscore> _highscores = new List<Highscore>();
    /// <summary>
    /// Зафиксированные рекорды в порядке убывания
    /// </summary>
    public Highscore[] Scores
    {
      get
      {
        return _highscores.ToArray();
      }
    }

    /// <summary>
    /// Конструктор модели таблицы рекордов
    /// </summary>
    private Highscores()
    {
      _highscores = new List<Highscore>();
      LoadHighscoresFromFile();
    }

    /// <summary>
    /// Загружает таблицу рекордов из файла
    /// </summary>
    private void LoadHighscoresFromFile()
    {
      ModelUtils.CreateAppDataHomeDirectoryIfNotExists();
      if (File.Exists(HighscoresFilename))
      {
        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream stream = new FileStream(HighscoresFilename, FileMode.Open, FileAccess.Read);
        try
        {
          _highscores.AddRange((IEnumerable<Highscore>)formatter.Deserialize(stream));
        }
        catch
        {
        }
        return;
      }
      using FileStream s = File.Create(HighscoresFilename);
    }

    /// <summary>
    /// Проверяет, являются ли набранные очки новым рекордом
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <returns>true - если поставлен новый рекорд, false - в противном случае</returns>
    public bool IsNewHighscore(int parScore)
    {
      if (parScore > 0)
      {
        if (_highscores.Count < MaxHighscoresCount)
        {
          return true;
        }
        return _highscores[^1].Score < parScore;
      }
      return false;
    }

    /// <summary>
    /// Добавляет новый рекорд в таблицу рекордов в порядке убывания.
    /// Если рекордов в таблице стало больше MaxHighscoresCount, то удаляет последний (наименьший) рекорд.
    /// Сохраняет обновлённую таблицу рекордов в файл
    /// </summary>
    /// <param name="parHighscore">Рекорд</param>
    public void AddHighscoreAndSaveToFile(Highscore parHighscore)
    {
      bool inserted = false;
      for (int i = 0; i < _highscores.Count; i++)
      {
        if (parHighscore.Score > _highscores[i].Score)
        {
          _highscores.Insert(i, parHighscore);
          inserted = true;
          break;
        }
      }
      if (!inserted)
      {
        _highscores.Add(parHighscore);
      }
      if (_highscores.Count > MaxHighscoresCount)
      {
        _highscores.RemoveAt(MaxHighscoresCount);
      }
      SaveHighscoresToFile();
    }

    /// <summary>
    /// Сохраняет таблицу рекордов в файл
    /// </summary>
    private void SaveHighscoresToFile()
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using FileStream stream = new FileStream(HighscoresFilename, FileMode.Open, FileAccess.Write);
      formatter.Serialize(stream, _highscores);
    }
  }
}