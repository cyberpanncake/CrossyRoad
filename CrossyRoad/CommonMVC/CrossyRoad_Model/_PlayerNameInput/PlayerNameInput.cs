using System.Collections.Generic;

namespace CrossyRoad_Model._PlayerNameInput
{
  /// <summary>
  /// Модель вводимого имя игрока
  /// </summary>
  public class PlayerNameInput
  {
    /// <summary>
    /// Символы вводимого имени игрока
    /// </summary>
    private readonly List<char> _symbols;
    /// <summary>
    /// Текущее значение имени игрока
    /// </summary>
    public string Name
    {
      get
      {
        return string.Join("", _symbols);
      }
    }
    /// <summary>
    /// Допустимость введённого имени игрока
    /// </summary>
    public bool IsValid
    {
      get
      {
        return !string.IsNullOrWhiteSpace(Name);
      }
    }
    /// <summary>
    /// Делегат изменения вводимого имени игрока
    /// </summary>
    public delegate void dChanged();
    /// <summary>
    /// Событие изменения вводимого имени игрока
    /// </summary>
    public event dChanged Changed = null;

    /// <summary>
    /// Конструктор модели вводимого имени игрока
    /// </summary>
    public PlayerNameInput()
    {
      _symbols = new List<char>();
    }

    /// <summary>
    /// Добавляет символ в конец имени игрока, если не превышена максимальная длина имени
    /// и символ является допустимым
    /// </summary>
    /// <param name="parSymbol">Добавляемый символ</param>
    /// <returns>true - если символ был добавлен, false - в противном случае</returns>
    public bool AddSymbol(char parSymbol)
    {
      if (_symbols.Count < ModelConfiguration.MAX_PLAYER_NAME_LENGTH && IsSymbolAllowed(parSymbol))
      {
        _symbols.Add(parSymbol);
        Changed?.Invoke();
        return true;
      }
      return false;
    }

    /// <summary>
    /// Удаляет последний введённый символ имени, если имя не пустое
    /// </summary>
    public void DeleteLastSymbol()
    {
      if (_symbols.Count > 0)
      {
        _symbols.RemoveAt(_symbols.Count - 1);
        Changed?.Invoke();
      }
    }

    /// <summary>
    /// Проверяет, является ли символ допустимым
    /// </summary>
    /// <param name="parSymbol">Проверяемый символ</param>
    /// <returns>true - если символ является допустимым, false - в противном случае</returns>
    private static bool IsSymbolAllowed(char parSymbol)
    {
      return !char.IsControl(parSymbol);
    }
  }
}