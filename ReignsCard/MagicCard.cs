using Lab1Kurs2;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// подкласс карты магии, влияют на значения на карте королевства
/// </summary>
public class MagicCard : Reigns
{
    #region fields and properties
    /// <summary>
    /// мана карты = чем больше тем выше шнас правильного использования, иначе противополодные значения
    /// </summary>
    private int manaChanceForWork;
    public int ManaChance
    {
        get { return manaChanceForWork; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -99 && intValue <= 99) manaChanceForWork = intValue;
                else throw CardException.InvalidMana(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    #endregion
    #region Constructor
    /// <summary>
    /// конутруктор для карт с фиксированными значениями
    /// </summary>
    /// <param name="id">id карты</param>
    /// <param name="name">название карты</param>
    /// <param name="opis">описание карты</param>
    /// <param name="manaChance">шанас правильного применнения</param>
    /// <param name="churchEffect">влияние на цервковь</param>
    /// <param name="peopleEffect">влияние на народ</param>
    /// <param name="armyEffect">влияние на армию</param>
    /// <param name="moneyEffect">влияние на казну</param>
    public MagicCard(int id, string name, string opis, int manaChance, int churchEffect, int peopleEffect, int armyEffect, int moneyEffect)
        : base(id, name, opis, churchEffect, peopleEffect, armyEffect, moneyEffect)
    {
        ManaChance = manaChance;
    }
    /// <summary>
    /// конструктор для карт наследников
    /// </summary>
    public MagicCard(int id, string name, string opis, int manaChance) : base(id, name, opis, 0, 0, 0, 0)
    {
        ManaChance = manaChance;
    }
    /// <summary>
    /// базовый контсруктор
    /// </summary>
    public MagicCard() : base()
    {
        ManaChance = 50;
    }
    #endregion
    #region Methods
    /// <summary>
    /// вывести карту магии
    /// </summary>
    public override void PrintCard()
    {
        base.PrintCard();
        Console.WriteLine($"Сложность выполнения: {manaChanceForWork}");
    }
    /// <summary>
    /// Проверить, сработает ли магическая карта (шанс на основе ManaChance)
    /// </summary>
    public virtual bool CanUseOrNot()
    {
        int chance = random.Next(1, 101);
        bool success = chance <= ManaChance;
        return success;
    }
    /// <summary>
    /// Применить эффект магической карты к королевству
    /// </summary>
    public override void UseCard(KingdomCard kingdom)
    {
        if (CanUseOrNot())
        {
            kingdom.IzmAspCard(kingdom.Church + Church, kingdom.People + People, kingdom.Army + Army, kingdom.Money + Money);
        }
        else
        {
            kingdom.IzmAspCard(kingdom.Church - Church, kingdom.People - People, kingdom.Army - Army, kingdom.Money - Money);
        }
    }
    /// <summary>
    /// значение суммы влияния на все аспекты
    /// </summary>
    public override int GetTotalIzm()
    {
        return Math.Abs(Church) + Math.Abs(People) + Math.Abs(Army) + Math.Abs(Money);
    }
    /// <summary>
    /// Является ли карта положительной (сумма эффектов > 0)
    /// </summary>
    public override bool IsPositiveCard()
    {
        return (Church + People + Army + Money) > 0;
    }
    /// <summary>
    /// Изменить эффекты магической карты
    /// </summary>
    public override void IzmAspCard(int church, int people, int army, int money)
    {
        Church = church;
        People = people;
        Army = army;
        Money = money;
    }
    #endregion
}
/// <summary>
/// подкласс карт магии, добавлен атрибут урон дополнительно наносит по армии
/// </summary>
public class BattleMagicCard : MagicCard
{
    #region поля
    private int Damage;
    public int damage
    {
        get { return Damage; }
        set
        {
            if (value is int intValue)
            {
                if (intValue >= -50 && intValue <= 50) Damage = intValue;
                else throw CardException.InvalidDMG(Id, Name);
            }
            else throw CardException.InvalidAspType(Id, Name);
        }
    }
    #endregion
    #region конструктор
    public BattleMagicCard(int id, string name, string opis, int manaChance, int damage, int churchEffect, int peopleEffect, int armyEffect, int moneyEffect)
        : base(id, name, opis, manaChance, churchEffect, peopleEffect, armyEffect, moneyEffect)
    {
        this.damage = damage;
    }
    public BattleMagicCard() : base()
    {
        damage = 10;
    }
    #endregion
    #region методы
    public override void PrintCard()
    {
        base.PrintCard();
        Console.WriteLine($"Урон: {Damage}");
    }
    public override void UseCard(KingdomCard kingdom)
    {
        if (CanUseOrNot())
            kingdom.IzmAspCard(kingdom.Church + Church, kingdom.People + People, kingdom.Army + Army - damage, kingdom.Money + Money);
        else
            kingdom.IzmAspCard(kingdom.Church - Church, kingdom.People - People, kingdom.Army - Army - damage, kingdom.Money - Money);
    }
    #endregion
}