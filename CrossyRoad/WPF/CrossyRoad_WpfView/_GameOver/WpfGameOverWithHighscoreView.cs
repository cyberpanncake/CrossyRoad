using CrossyRoad_Model._PlayerNameInput;
using CrossyRoad_View._GameOver;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossyRoad_WpfView._GameOver
{
  /// <summary>
  /// Представление окна окончания игры с новым рекордом в приложении WPF
  /// </summary>
  public class WpfGameOverWithHighscoreView : GameOverWithHighscoreView
  {
    /// <summary>
    /// Графический элемент окна окончания игры
    /// </summary>
    private Grid _element;
    /// <summary>
    /// Графический элемент поля ввода имени игрока
    /// </summary>
    private TextBlock _playerNameInput;
    /// <summary>
    /// Графический элемент сообщения об ошибке ввода
    /// </summary>
    private TextBlock _errorMessage;

    /// <summary>
    /// Конструктор представления окна окончания игры с новым рекордом в приложении WPF
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    /// <param name="parPlayerNameInput">Модель вводимого имени игрока</param>
    public WpfGameOverWithHighscoreView(int parScore, PlayerNameInput parPlayerNameInput)
      : base(parScore, parPlayerNameInput)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _element = new Grid
        {
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0)
        };
        _element.RowDefinitions.Add(new RowDefinition());
        _element.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        _element.RowDefinitions.Add(new RowDefinition());
        StackPanel panel = new StackPanel();
        Grid.SetRow(panel, 1);
        Grid.SetColumn(panel, 1);
        _ = _element.Children.Add(panel);
        panel.Children.Add(new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_Highscore,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        });
        panel.Children.Add(new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_Score + Score,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        });
        panel.Children.Add(new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_Highscore_EnterName,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        });
        _playerNameInput = new TextBlock()
        {
          Background = Brushes.White,
          Foreground = Brushes.Black,
          Width = 200,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        };
        panel.Children.Add(_playerNameInput);
        _errorMessage = new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_Highscore_InvalidName,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        };
        panel.Children.Add(_errorMessage);
      });
    }

    /// <summary>
    /// Отображает окно окончания игры с новым рекордом на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_GameOver_Highscore);
        WpfUtils.DrawOnWindow(_element);
      });
    }

    /// <summary>
    /// Отображает текущее введённое имя игрока
    /// </summary>
    public override void RedrawPlayerName()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _playerNameInput.Text = PlayerNameInput.Name;
        _errorMessage.Text = PlayerNameInput.IsValid ? "" : CrossyRoad_View.Properties.Resources.GameOver_Highscore_InvalidName;
      });
    }
  }
}