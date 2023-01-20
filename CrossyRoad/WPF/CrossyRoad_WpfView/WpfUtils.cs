using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CrossyRoad_WpfView
{
  /// <summary>
  /// Утилита для работы с окном WPF
  /// </summary>
  public static class WpfUtils
  {
    /// <summary>
    /// Окно
    /// </summary>
    private static readonly Window _window;
    /// <summary>
    /// Самый родительский контейнер, на котором отображаются объекты
    /// </summary>
    private static readonly Panel _parent;
    /// <summary>
    /// Панель, на которой находятся элементы в верхней части окна
    /// </summary>
    private static readonly Panel _topPanel;
    /// <summary>
    /// Текущая ширина окна (пиксели)
    /// </summary>
    public static float WindowWidth => (float)_window.ActualWidth;
    /// <summary>
    /// Текущая высота окна (пиксели)
    /// </summary>
    public static float WindowHeight => (float)_window.ActualHeight;
    /// <summary>
    /// Диспетчер окна WPF
    /// </summary>
    public static Dispatcher Dispatcher => _window.Dispatcher;

    /// <summary>
    /// Конструктор утилиты для работы с WPF
    /// </summary>
    static WpfUtils()
    {
      _parent = new DockPanel
      {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        VerticalAlignment = VerticalAlignment.Stretch,
        Margin = new Thickness(WpfViewConfiguration.DEFAULT_MARGIN)
      };
      _window = new Window
      {
        Background = WpfViewConfiguration.DefaultBackgroundColor,
        Foreground = WpfViewConfiguration.DefaultForegroundColor,
        FontSize = WpfViewConfiguration.DEFAULT_FONT_SIZE,
        Content = _parent,
        Title = CrossyRoad_View.Properties.Resources.Game_Name
      };
      _topPanel = new Grid
      {
        Margin = new Thickness(0, 0, 0, 2 * WpfViewConfiguration.DEFAULT_MARGIN)
      };
      DockPanel.SetDock(_topPanel, Dock.Top);
      _parent.Children.Add(_topPanel);
    }

    #region Управление окном WPF
    /// <summary>
    /// Инициализирует и отображает окно приложения WPF
    /// </summary>
    public static void InitializeAndShowWindow()
    {
      Dispatcher.Invoke(() =>
      {
        _window.MinWidth = WpfViewConfiguration.WINDOW_MIN_WIDTH;
        _window.Width = WpfViewConfiguration.WINDOW_START_WIDTH;
        _window.MinHeight = WpfViewConfiguration.WINDOW_MIN_HEIGHT;
        _window.Height = WpfViewConfiguration.WINDOW_START_HEIGHT;
        _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _window.Show();
      });
    }

    /// <summary>
    /// Устанавливает режим изменения размеров окна WPF
    /// </summary>
    /// <param name="parCanResize">true - можно изменять размеры, false - нельзя</param>
    public static void SetWindowCanResize(bool parCanResize)
    {
      Dispatcher.Invoke(() =>
      {
        _window.ResizeMode = parCanResize ? ResizeMode.CanResize : ResizeMode.NoResize;
      });
    }

    /// <summary>
    /// Закрывает окно приложения
    /// </summary>
    public static void CloseWindow()
    {
      Dispatcher.Invoke(() =>
      {
        _window.Close();
      });
    }

    #region Управление обработчиками событий окна WPF
    /// <summary>
    /// Добавляет обработчик нажатия клавиши окна приложения WPF
    /// </summary>
    /// <param name="parKeyDownHandler">Обработчик нажатия клавиши</param>
    public static void AddKeyDownHandlerToWindow(KeyEventHandler parKeyDownHandler)
    {
      Dispatcher.Invoke(() =>
      {
        _window.KeyDown += parKeyDownHandler;
      });
    }

    /// <summary>
    /// Удаляет обработчик нажатия клавиши окна приложения WPF
    /// </summary>
    /// <param name="parKeyDownHandler">Обработчик нажатия клавиши</param>
    public static void RemoveKeyDownHandlerToWindow(KeyEventHandler parKeyDownHandler)
    {
      Dispatcher.Invoke(() =>
      {
        _window.KeyDown -= parKeyDownHandler;
      });
    }

    /// <summary>
    /// Добавляет обработчик ввода текста приложения WPF
    /// </summary>
    /// <param name="parTextInputHandler">Обработчик ввода текста</param>
    public static void AddPreviewTextInputHandlerToWindow(TextCompositionEventHandler parTextInputHandler)
    {
      _window.PreviewTextInput += parTextInputHandler;
    }

    /// <summary>
    /// Удаляет обработчик ввода текста приложения WPF
    /// </summary>
    /// <param name="parTextInputHandler">Обработчик ввода текста</param>
    public static void RemovePreviewTextInputHandlerToWindow(TextCompositionEventHandler parTextInputHandler)
    {
      _window.PreviewTextInput -= parTextInputHandler;
    }

    /// <summary>
    /// Добавляет обработчик изменения размеров окна приложения WPF
    /// </summary>
    /// <param name="parSizeChangedHandler">Обработчик изменения размеров окна</param>
    public static void AddSizeChangedHandlerToWindow(SizeChangedEventHandler parSizeChangedHandler)
    {
      _window.SizeChanged += parSizeChangedHandler;
    }

    /// <summary>
    /// Удаляет обработчик изменения размеров окна приложения WPF
    /// </summary>
    /// <param name="parSizeChangedHandler">Обработчик изменения размеров окна</param>
    public static void RemoveSizeChangedHandlerToWindow(SizeChangedEventHandler parSizeChangedHandler)
    {
      _window.SizeChanged -= parSizeChangedHandler;
    }
    #endregion
    #endregion

    #region Отображение графических элементов на окне WPF
    /// <summary>
    /// Очищает окно приложения
    /// </summary>
    public static void ClearWindow()
    {
      Dispatcher.Invoke(() =>
      {
        _parent.Children.Clear();
        _topPanel.Children.Clear();
        _parent.Children.Add(_topPanel);
      });
    }

    /// <summary>
    /// Отображает графический элемент на экране приложения WPF
    /// </summary>
    /// <param name="parElement">Графический элемент</param>
    public static void DrawOnWindow(UIElement parElement)
    {
      Dispatcher.Invoke(() =>
      {
        if (!_parent.Children.Contains(parElement))
        {
          _ = _parent.Children.Add(parElement);
        }
      });
    }

    /// <summary>
    /// Отображает графический элемент в верхней части экрана приложения WPF
    /// </summary>
    /// <param name="parElement">Графический элемент</param>
    public static void DrawOnTop(UIElement parElement)
    {
      Dispatcher.Invoke(() =>
      {
        if (!_topPanel.Children.Contains(parElement))
        {
          _ = _topPanel.Children.Add(parElement);
        }
      });
    }

    /// <summary>
    /// Отображает текст подсказки на экране приложения WPF
    /// </summary>
    /// <param name="parText">Текст подсказки</param>
    public static void DrawHelpText(string parText)
    {
      Dispatcher.Invoke(() =>
      {
        TextBlock helpTextElement = new TextBlock
        {
          Text = parText,
          FontSize = WpfViewConfiguration.HELP_TEXT_FONT_SIZE,
          HorizontalAlignment = HorizontalAlignment.Right
        };
        _ = _topPanel.Children.Add(helpTextElement);
      });
    }

    /// <summary>
    /// Отображает текст на экране приложения WPF, отцентрованный по горизонтали,
    /// в верхней части экрана под уже существующими элементами
    /// </summary>
    /// <param name="parText">Текст</param
    public static void DrawCenteredText(string parText)
    {
      Dispatcher.Invoke(() =>
      {
        TextBlock textElement = new TextBlock
        {
          Text = parText,
          HorizontalAlignment = HorizontalAlignment.Center,
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0)
        };
        DockPanel.SetDock(textElement, Dock.Top);
        _ = _parent.Children.Add(textElement);
      });
    }
    #endregion
  }
}