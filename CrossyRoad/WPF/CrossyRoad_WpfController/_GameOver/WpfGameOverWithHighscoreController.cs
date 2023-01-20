using CrossyRoad_Controller._GameOver;
using CrossyRoad_Model._Highscores;
using CrossyRoad_Model._PlayerNameInput;
using CrossyRoad_WpfController._Menu;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._GameOver;
using System.Windows.Input;

namespace CrossyRoad_WpfController._GameOver
{
  /// <summary>
  /// Контроллер окна окончания игры с новым рекордом в приложении WPF
  /// </summary>
  public class WpfGameOverWithHighscoreController : GameOverWithHighscoreController
  {
    /// <summary>
    /// Конструктор контроллера окна окончания игры с новым рекордом в приложении WPF
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    public WpfGameOverWithHighscoreController(int parScore, PlayerNameInput parPlayerNameInput)
      : base(parScore, parPlayerNameInput, new WpfGameOverWithHighscoreView(parScore, parPlayerNameInput))
    {
    }

    /// <summary>
    /// Запускает переход в окно окончания игры с новым рекордом под управлением контроллера
    /// </summary>
    public override void Start()
    {
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
      WpfUtils.AddPreviewTextInputHandlerToWindow(PreviewTextInputEventHandler);
      PlayerNameInput.Changed += GameOverWithHighscoreView.RedrawPlayerName;
      GameOverWithHighscoreView.Draw();
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
        WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
        WpfUtils.RemovePreviewTextInputHandlerToWindow(PreviewTextInputEventHandler);
        new WpfMenuMainController().Start();
      }
      if (args.Key == Key.Enter && PlayerNameInput.IsValid)
      {
        Highscores.Instance.AddHighscoreAndSaveToFile(new Highscore(PlayerNameInput.Name, Score));
        WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
        WpfUtils.RemovePreviewTextInputHandlerToWindow(PreviewTextInputEventHandler);
        new WpfMenuMainController().Start();
      }
    }

    /// <summary>
    /// Обработчик события ввода текста
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Аргументы события ввода текста</param>
    private void PreviewTextInputEventHandler(object sender, TextCompositionEventArgs e)
    {
      if (e.Text.Equals("\b"))
      {
        PlayerNameInput.DeleteLastSymbol();
        return;
      }
      foreach (char elChar in e.Text)
      {
        PlayerNameInput.AddSymbol(elChar);
      }
    }
  }
}