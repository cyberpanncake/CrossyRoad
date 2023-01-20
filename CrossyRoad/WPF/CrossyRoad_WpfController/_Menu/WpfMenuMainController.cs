using CrossyRoad_Controller._Menu;
using CrossyRoad_Model;
using CrossyRoad_Model._Game;
using CrossyRoad_Model._Menu;
using CrossyRoad_WpfController._Game;
using CrossyRoad_WpfController._Highscores;
using CrossyRoad_WpfController._Information;
using CrossyRoad_WpfView;
using CrossyRoad_WpfView._Menu;
using System.Threading;
using System.Windows.Input;

namespace CrossyRoad_WpfController._Menu
{
  /// <summary>
  /// Контроллер окна главного меню в приложении WPF
  /// </summary>
  public class WpfMenuMainController : MenuController
  {
    /// <summary>
    /// Конструктор контроллера окна главного меню в приложении WPF
    /// </summary>
    public WpfMenuMainController()
      : base(MenuMain.Instance, new WpfMenuMainView())
    {
    }

    /// <summary>
    /// Запускает переход в главное меню под управлением контроллера
    /// </summary>
    public override void Start()
    {
      SelectedItemToFocused();
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Game].Selected += StartGame;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Highscores].Selected += StartHighscores;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Information].Selected += StartInformation;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Exit].Selected += Exit;
      WpfUtils.AddKeyDownHandlerToWindow(KeyDownEventHandler);
      MenuView.Draw();
    }

    /// <summary>
    /// Изменяет состояние выбранного пункта меню на состояние "в фокусе"
    /// </summary>
    private void SelectedItemToFocused()
    {
      foreach (MenuItem elItem in Menu.Items)
      {
        if (elItem.State == MenuItemState.Selected)
        {
          elItem.State = MenuItemState.Focused;
          return;
        }
      }
    }

    /// <summary>
    /// Обработчик события нажатия клавиши
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="args">Аргументы события нажатия клавиши</param>
    public void KeyDownEventHandler(object sender, KeyEventArgs args)
    {
      switch (args.Key)
      {
        case Key.Up:
          Menu.MoveFocusToPreviousItem();
          break;
        case Key.Down:
          Menu.MoveFocusToNextItem();
          break;
        case Key.Enter:
          Menu.SelectCurrentItem();
          break;
      }
    }

    /// <summary>
    /// Удаляет обработчики событий модели главного меню и окна
    /// </summary>
    private void ClearHandlers()
    {
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Game].Selected -= StartGame;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Highscores].Selected -= StartHighscores;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Information].Selected -= StartInformation;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Exit].Selected -= Exit;
      WpfUtils.RemoveKeyDownHandlerToWindow(KeyDownEventHandler);
    }

    /// <summary>
    /// Запускает переход в игру
    /// </summary>
    private void StartGame()
    {
      ClearHandlers();
      new WpfGameController(new Game()).Start();
    }

    /// <summary>
    /// Запускает переход в таблицу рекордов
    /// </summary>
    private void StartHighscores()
    {
      ClearHandlers();
      new WpfHighscoresController().Start();
    }

    /// <summary>
    /// Запускает переход в справку
    /// </summary>
    private void StartInformation()
    {
      ClearHandlers();
      new WpfInformationController().Start();
    }

    /// <summary>
    /// Завершает работу приложения
    /// </summary>
    private void Exit()
    {
      Thread.Sleep(500);
      WpfUtils.CloseWindow();
    }
  }
}