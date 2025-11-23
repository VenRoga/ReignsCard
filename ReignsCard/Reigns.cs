using System;

/// <summary>
/// Абстрактный класс карт
/// </summary>
public abstract class Reigns : IComparable<Reigns>
{
    #region Fields and properties
    protected static Random random = new Random();
    private int IdForWork;
    public int Id
    {
        get { return IdForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= 0 && intValue <= 999) IdForWork = intValue;
                else throw CardException.InvalidId(intValue, Name ?? "...");
            }
            else throw CardException.InvalidIdType(0, Name ?? "...");
        }
    }
    private string NameForWork;
    public string Name
    {
        get { return NameForWork; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value)) NameForWork = value;
            else throw CardException.InvalidName(Id, value ?? "...");
        }
    }
    private string OpisForWork;
    public string Opis
    {
        get { return OpisForWork; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value)) OpisForWork = value;
            else throw CardException.InvalidName(Id, value ?? "...");
        }
    }
    private int ChurchForWork;
    public int Church
    {
        get { return ChurchForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -99 && intValue <= 99) ChurchForWork = intValue;
                else throw CardException.InvalidAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int PeopleForWork;
    public int People
    {
        get { return PeopleForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -99 && intValue <= 99) PeopleForWork = intValue;
                else throw CardException.InvalidAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int ArmyForWork;
    public int Army
    {
        get { return ArmyForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -99 && intValue <= 99) ArmyForWork = intValue;
                else throw CardException.InvalidAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    private int MoneyForWork;
    public int Money
    {
        get { return MoneyForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -99 && intValue <= 99) MoneyForWork = intValue;
                else throw CardException.InvalidAsp(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    #endregion
    #region Constructor
    /// <summary>
    /// конструктор с вводимыми параметроами
    /// </summary>
    /// <param name="id">id карты</param>
    /// <param name="name">название карты</param>
    /// <param name="opis">описание карты</param>
    /// <param name="church">занчение влияния церкви</param>
    /// <param name="people">значение влияния народ</param>
    /// <param name="army">значение влияния армия</param>
    /// <param name="money">значение влияния казна</param>
    protected Reigns(int id, string name, string opis, int church, int people, int army, int money)
    {
        Id = id;
        Name = name;
        Opis = opis;
        Church = church;
        People = people;
        Army = army;
        Money = money;
    }
    /// <summary>
    /// конструктор без параметров
    /// </summary>
    protected Reigns() : this(666, "Пустая карта", "Нет описания", 1, 1, 1, 1) { }
    #endregion
    #region Methods
    /// <summary>
    /// вывести карту
    /// </summary>
    public virtual void PrintCard()
    {
        Console.WriteLine($"ID: {IdForWork:D3}");
        Console.WriteLine($"Название: {NameForWork}");
        Console.WriteLine($"Описание: {OpisForWork}");
        Console.WriteLine($"Церковь: {ChurchForWork}");
        Console.WriteLine($"Народ: {PeopleForWork}");
        Console.WriteLine($"Армия: {ArmyForWork}");
        Console.WriteLine($"Казна: {MoneyForWork}");
        Console.WriteLine("___________________________________________________________________");
    }
    public int CompareTo(Reigns other)
    {
        int res = GetTypePriority(this).CompareTo(GetTypePriority(other));
        if (res != 0) return res;
        res = Id.CompareTo(other.Id); //по ID
        if (res != 0) return res;
        res = Church.CompareTo(other.Church); //по церкви
        if (res != 0) return res;
        res = People.CompareTo(other.People); //по народу
        if (res != 0) return res;
        res = Army.CompareTo(other.Army); //по армии
        if (res != 0) return res;
        res = Money.CompareTo(other.Money); //по казне
        if (res != 0) return res;
        return Name.CompareTo(other.Name); //по названию
    }
    private int GetTypePriority(Reigns card)
    {
        if (card is KingdomCard) return 1;
        if (card is MagicCard) return 2;
        if (card is BattleMagicCard) return 3;
        return 4;
    }
    public abstract void UseCard(KingdomCard kingdom);
    public abstract void IzmAspCard(int church, int people, int army, int money);
    public abstract int GetTotalIzm();
    public override string ToString()
    {
        return $"{NameForWork}: {OpisForWork} - Ц{ChurchForWork} Н{PeopleForWork} А{ArmyForWork} К{MoneyForWork}";
    }
    public virtual bool IsPositiveCard()
    {
        return (ChurchForWork + PeopleForWork + ArmyForWork + MoneyForWork) > 0;
    }
    /// <summary>
    /// сравнение по среднему влиянию на аспекты
    /// </summary>
    public static bool operator >(Reigns a, Reigns b)
    {
        return a.GetTotalIzm() > b.GetTotalIzm();
    }
    public static bool operator <(Reigns a, Reigns b)
    {
        return a.GetTotalIzm() < b.GetTotalIzm();
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || obj.GetType() != GetType()) return false;
        var s = (Reigns)obj;
        return Id == s.Id && NameForWork == s.NameForWork && OpisForWork == s.OpisForWork;
    }
    public static bool operator ==(Reigns a, Reigns b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }
    public static bool operator !=(Reigns a, Reigns b)
    {
        return !(a == b);
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + (NameForWork?.GetHashCode() ?? 0);
            hash = hash * 23 + (OpisForWork?.GetHashCode() ?? 0);
            return hash;
        }
    }
    #endregion
}