using CrossyRoad_Model._Highscores;

namespace CrossyRoad_View._Highscores
{
  /// <summary>
  /// Представление окна с таблицей рекордов (пункт меню "Рекорды")
  /// </summary>
  public abstract class HighscoresView : View
  {
    /// <summary>
    /// Модель таблицы рекордов
    /// </summary>
    protected Highscores Highscores { get; }

    /// <summary>
    /// Конструктор представления окна с таблицей рекордов
    /// </summary>
    /// <param name="parHighscores">Модель таблицы рекордов</param>
    public HighscoresView(Highscores parHighscores)
      : base()
    {
      Highscores = parHighscores;
    }
  }
}