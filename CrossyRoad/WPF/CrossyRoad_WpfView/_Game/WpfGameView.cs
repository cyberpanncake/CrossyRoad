using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game;
using CrossyRoad_View._Game.Objects;
using CrossyRoad_WpfView._Game.Objects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Game
{
  /// <summary>
  /// Представление окна с игровым процессом (пункт меню "Играть") в приложении WPF
  /// </summary>
  public class WpfGameView : GameView
  {
    /// <summary>
    /// Делегат окончания обратного отсчёта перед игрой
    /// </summary>
    public delegate void dCountDownEnded();
    /// <summary>
    /// Событие окончания обратного отсчёта перед игрой
    /// </summary>
    public event dCountDownEnded CountDownEnded = null;
    /// <summary>
    /// Графический элемент отображения набранных очков
    /// </summary>
    private TextBlock _scoreElement;

    /// <summary>
    /// Конструктор представления игрового процесса в консольном приложении
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public WpfGameView(Game parGame)
      : base(parGame)
    {
    }

    /// <summary>
    /// Отображает игровой процесс на экране приложения в первый раз
    /// </summary>
    public async void DrawWithCountDown()
    {
      await Task.Run(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_Game);
        ((WpfGameFieldView)GameFieldView).DrawFirstTime();
        Thread.Sleep(100);
        base.Draw();
        DrawScore(0);
        Thread.Sleep(3000);
        CountDownEnded?.Invoke();
      });
    }

    /// <summary>
    /// Создаёт представление игрового поля
    /// </summary>
    /// <param name="parGameField">Модель игрового поля</param>
    /// <returns>Представление игрового поля</returns>
    protected override GameFieldView CreateGameFieldView(GameField parGameField)
    {
      return new WpfGameFieldView(parGameField);
    }

    /// <summary>
    /// Отображает набранные очки на экране приложения
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    protected override async void DrawScore(int parScore)
    {
      await Task.Run(() =>
      {
        WpfUtils.Dispatcher.Invoke(() =>
        {
          if (_scoreElement == null)
          {
            _scoreElement = new TextBlock();
          }
          _scoreElement.Text = CrossyRoad_View.Properties.Resources.GameOver_Score + parScore.ToString();
          WpfUtils.DrawOnTop(_scoreElement);
        });
      });
    }
  }
}