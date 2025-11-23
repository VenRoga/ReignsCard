using System;

/// <summary>
/// Пользовательское исключение для карточных операций
/// </summary>
public class CardException : Exception
{
    #region поля
    public int CardId { get; }
    public string CardName { get; }
    #endregion
    #region сообщения
    /// <summary>
    /// сообщения ошибок
    /// </summary>
    private const string INVALID_ID_MESSAGE = "ID карты должен быть трехзначным числом (0-999)";
    private const string INVALID_DAMAGE_MESSAGE = "Урон должен быть в диапазоне -50 до 50";
    private const string INVALID_ID_TYPE_MESSAGE = "ID карты должен быть числом";
    private const string INVALID_NAME_MESSAGE = "Название карты не может быть пустым";
    private const string INVALID_ASP_MESSAGE = "Влияние должно быть в диапазоне -99 до 99";
    private const string INVALID_ASPTYPE_MESSAGE = "Влияние должно быть числом";
    private const string INVALID_KINGDOM_ASP_MESSAGE = "Апекты королевства должны быть от 1 до 99";
    private const string INVALID_MANA_MESSAGE = "Мана карты должна быть в диапазоне 0 до 100";
    #endregion
    #region конструктор
    /// <summary>
    /// создание сообщений об ошибках с необязательным полем системной ошибки
    /// </summary>
    public static CardException InvalidKingName(int cardId, string cardName = null, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_NAME_MESSAGE, cardId, cardName) : new CardException(INVALID_NAME_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidIdType(int cardId = 0, string cardName = null, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_ID_TYPE_MESSAGE, cardId, cardName) : new CardException(INVALID_ID_TYPE_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidId(int cardId, string cardName = null, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_ID_MESSAGE, cardId, cardName) : new CardException(INVALID_ID_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidName(int cardId, string cardName = null, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_NAME_MESSAGE, cardId, cardName) : new CardException(INVALID_NAME_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidAsp(int cardId, string cardName, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_ASP_MESSAGE, cardId, cardName) : new CardException(INVALID_ASP_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidAspType(int cardId, string cardName, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_ASP_MESSAGE, cardId, cardName) : new CardException(INVALID_ASP_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidKingdomAsp(int cardId, string cardName, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_KINGDOM_ASP_MESSAGE, cardId, cardName) : new CardException(INVALID_KINGDOM_ASP_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidMana(int cardId, string cardName, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_MANA_MESSAGE, cardId, cardName) : new CardException(INVALID_MANA_MESSAGE, cardId, cardName, innerException);
    public static CardException InvalidDMG(int cardId, string cardName, Exception innerException = null)
    => innerException == null ? new CardException(INVALID_MANA_MESSAGE, cardId, cardName) : new CardException(INVALID_MANA_MESSAGE, cardId, cardName, innerException);
    /// <summary>
    /// для обычных исключений
    /// </summary>
    public CardException(string message, int cardId, string cardName)
        : base(message)
    {
        CardId = cardId;
        CardName = cardName ?? "...";
    }
    /// <summary>
    /// для вложенных исключений с системными
    /// </summary>
    public  CardException(string message, int cardId, string cardName, Exception innerException)
        : base(message, innerException)
    {
        CardId = cardId;
        CardName = cardName ?? "...";
    }
    #endregion
    #region методы
    public override string ToString()
    {
        return $"CardException: {Message} (ID: {CardId}, Карта: {CardName})";
    }
    #endregion
}