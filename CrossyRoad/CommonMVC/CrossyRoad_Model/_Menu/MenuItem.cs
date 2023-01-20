namespace CrossyRoad_Model._Menu
{
  /// <summary>
  /// Модель пункта меню
  /// </summary>
  public class MenuItem
  {
    /// <summary>
    /// Делегат изменения состояния пункта меню
    /// </summary>
    public delegate void dChanged();
    /// <summary>
    /// Событие изменения состояния пункта меню
    /// </summary>
    public event dChanged Changed = null;
    /// <summary>
    /// Делегат выбора пункта меню
    /// </summary>
    public delegate void dSelected();
    /// <summary>
    /// Событие выбора пункта меню
    /// </summary>
    public event dSelected Selected = null;
    /// <summary>
    /// Состояние пункта меню
    /// </summary>
    private MenuItemState _state;
    /// <summary>
    /// Имя пункта меню
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Состояние пункта меню
    /// </summary>
    public MenuItemState State
    {
      get
      {
        return _state;
      }
      set
      {
        _state = value;
        Changed?.Invoke();
        if (_state == MenuItemState.Selected)
        {
          Selected?.Invoke();
        }
        return;
      }
    }

    /// <summary>
    /// Конструктор модели пункта меню
    /// </summary>
    /// <param name="parName">Имя пункта меню</param>
    public MenuItem(string parName)
    {
      Name = parName;
      _state = MenuItemState.Default;
    }
  }
}