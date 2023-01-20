using CrossyRoad_Model._Highscores;
using CrossyRoad_View._Highscores;

namespace CrossyRoad_Controller._Highscores
{
  /// <summary>
  /// Контроллер окна с таблицей рекордов (пункт меню "Рекорды")
  /// </summary>
  public abstract class HighscoresController : Controller
  {
    /// <summary>
    /// Модель таблицы рекордов
    /// </summary>
    protected Highscores Highscores { get; }
    /// <summary>
    /// Представление окна с таблицей рекордов
    /// </summary>
    protected HighscoresView HighscoresView { get; }

    /// <summary>
    /// Конструктор контроллера окна с таблицей рекордов (пункт меню "Рекорды")
    /// </summary>
    /// <param name="parHighscores">Модель таблицы рекордов</param>
    /// <param name="parHighscoresView">Представление  окна с таблицей рекордов</param>
    public HighscoresController(Highscores parHighscores, HighscoresView parHighscoresView)
      : base()
    {
      Highscores = parHighscores;
      HighscoresView = parHighscoresView;
    }
  }
}