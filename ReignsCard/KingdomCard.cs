using Lab1Kurs2;
using System.Xml.Linq;
using System;
using System.Security.Policy;

/// <summary>
/// подкласс карта королеввства, карты действуют именно на этот класс
/// </summary>
public class KingdomCard : Reigns
{
    #region Fields and properties
    /// <summary>
    /// имя короля
    /// </summary>
    private string KingNameKingdom;
    public string KingName
    {
        get { return KingNameKingdom; }
        set { KingNameKingdom = value; }
    }
    private int churchKingdomForWork;
    public int ChurchKingdom
    {
        get { return churchKingdomForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= 1 && intValue <= 99) churchKingdomForWork = intValue;
                else throw CardException.InvalidKingdomAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int peopleKingdomForWork;
    public int PeopleKingdom
    {
        get { return peopleKingdomForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= 1 && intValue <= 99) peopleKingdomForWork = intValue;
                else throw CardException.InvalidKingdomAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int armyKingdomForWork;
    public int ArmyKingdom
    {
        get { return armyKingdomForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= 1 && intValue <= 99) armyKingdomForWork = intValue;
                else throw CardException.InvalidKingdomAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int moneyKingdomForWork;
    public int MoneyKingdom
    {
        get { return moneyKingdomForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= 1 && intValue <= 99) moneyKingdomForWork = intValue;
                else throw CardException.InvalidKingdomAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    #endregion
    #region Constructor
    /// <summary>
    /// конструктор c вводимымми параметрами
    /// </summary>
    /// <param name="kingName">имя короля</param>
    /// <param name="church">значение церкви в королевстве</param>
    /// <param name="people">значение народа в королевстве</param>
    /// <param name="army">значение армии в королевстве</param>
    /// <param name="money">значение казны в королевстве</param>
    public KingdomCard(string kingName, int church, int people, int army, int money) : base()
    {
        KingName = kingName;
        ChurchKingdom = church;
        PeopleKingdom = people;
        ArmyKingdom = army;
        MoneyKingdom = money;

        Church = ChurchKingdom;
        People = PeopleKingdom;
        Army = ArmyKingdom;
        Money = MoneyKingdom;
    }
    public KingdomCard() : base()
    {
        KingName = "Нет имени короля";
        ChurchKingdom = 50;
        PeopleKingdom = 50;
        ArmyKingdom = 50;
        MoneyKingdom = 50;

        Church = ChurchKingdom;
        People = PeopleKingdom;
        Army = ArmyKingdom;
        Money = MoneyKingdom;
    }
    #endregion
    #region Methods
    /// <summary>
    /// вывести карту королевства
    /// </summary>
    public override void PrintCard()
    {
        Console.WriteLine($"=== КОРОЛЕВСТВО ===");
        Console.WriteLine($"Король: {KingNameKingdom}");
        Console.WriteLine($"Церковь: {churchKingdomForWork}");
        Console.WriteLine($"Народ: {peopleKingdomForWork}");
        Console.WriteLine($"Армия: {armyKingdomForWork}");
        Console.WriteLine($"Казна: {moneyKingdomForWork}");
        Console.WriteLine("___________________________________________________________________");
    }
    /// <summary>
    /// Применить магическую карту (одноразовое применение)
    /// </summary>
    public void UseMagicCard(MagicCard magicCard)
    {
        magicCard.UseCard(this);
    }
    /// <summary>
    /// Сбросить все изменения к исходным значениям
    /// </summary>
    public void ResetToBaseValues()
    {
        Church = ChurchKingdom;
        People = PeopleKingdom;
        Army = ArmyKingdom;
        Money = MoneyKingdom;
    }
    /// <summary>
    /// Использовать карту на королевстве
    /// </summary>
    public override void UseCard(KingdomCard kingdom)
    {
        // начать листать колоду(добавить)
    }
    /// <summary>
    /// изменить значения карты королевства
    /// </summary>
    /// <param name="church">изменение церкви</param>
    /// <param name="people">изменение народа</param>
    /// <param name="army">изменение армии</param>
    /// <param name="money">изменение казны</param>
    public override void IzmAspCard(int church, int people, int army, int money)
    {
        Church = church;
        People = people;
        Army = army;
        Money = money;
    }
    /// <summary>
    /// получить значение изменения аспектов от базовых
    /// </summary>
    public override int GetTotalIzm()
    {
        return Math.Abs(ChurchKingdom - Church) + Math.Abs(PeopleKingdom - People) + Math.Abs(ArmyKingdom - Army) + Math.Abs(MoneyKingdom - Money);
    }
    #endregion
}