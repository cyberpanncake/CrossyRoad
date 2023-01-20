using CrossyRoad_Model._Game;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_View._Game;
using CrossyRoad_View._Game.Objects;
using CrossyRoad_WpfView._Game.Objects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
    /// Графический элемент отображения обратного отсчёта
    /// </summary>
    private TextBlock _countDownElement;

    /// <summary>
    /// Конструктор представления игрового процесса в консольном приложении
    /// </summary>
    /// <param name="parGame">Модель игрового процесса</param>
    public WpfGameView(Game parGame)
      : base(parGame)
    {
      _countDownElement = new TextBlock
      {
        FontSize = 40,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid.SetColumn(_countDownElement, 1);
      _ = ((WpfGameFieldView)GameFieldView).Element.Children.Add(_countDownElement);
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
        DrawCountDown();
        CountDownEnded?.Invoke();
      });
    }

    private void DrawCountDown()
    {
      for (int i = 3; i > 0; i--)
      {
        WpfUtils.Dispatcher.Invoke(() =>
        {
          _countDownElement.Text = i.ToString();
        });
        Thread.Sleep(1000);
      }
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _countDownElement.Text = "Старт!";
      });
      Thread.Sleep(200);
      WpfUtils.Dispatcher.Invoke(() =>
      {
        ((WpfGameFieldView)GameFieldView).Element.Children.Remove(_countDownElement);
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