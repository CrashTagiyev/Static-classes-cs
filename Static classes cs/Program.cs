using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

abstract public class BankCard
{
    //Constructor
    protected BankCard(string pin)
    {
        History = new List<string?> { };
        PIN = pin;
        ExpireDate = new DateTime(DateTime.Now.Year + 3, DateTime.Now.Month, DateTime.Now.Day);
        RandomizePAN();
        RandomizeCVC();
        RandomizeBalance();
    }
    //Properties
    abstract protected string? PAN { get; set; }
    abstract public string? PIN { get; set; }
    abstract protected string? CVC { get; set; }
    abstract protected DateTime ExpireDate { get; set; }
    abstract protected float Balance { get; set; }
    abstract public List<string?> History { get; set; }
    //Methods
    public override string ToString()
    {
        return $"Card name:{this.GetType()}\nPan:{PAN}\nCVC:{CVC}\nPIN:{PIN}\nBalance:{(int)Balance} AZN  {(int)((Balance - (int)Balance) * 100)} qepik\nExpire date:{ExpireDate.ToShortDateString()}\n";
    }
    private void RandomizeCVC()
    {
        Random random = new Random();
        CVC = Convert.ToString(random.Next(100, 999));
    }
    private void RandomizePAN()
    {
        Random random = new Random();
        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {

                PAN += Convert.ToString(random.Next(1, 9));
            }
            else PAN += Convert.ToString(random.Next(0, 9));
        }
    }
    private void RandomizeBalance()
    {
        Random random = new Random();
        float Min = 100;
        float Max = 5000;
        Balance = (float)(random.NextDouble() * (Max - Min) + Min);
    }
    private void RandomizePIN()
    {
        Random random = new Random();
        PIN = Convert.ToString(random.Next(1000, 9999));
    }
    //virtuals
    virtual public void ChangePIN(string newPIN)
    {
        int result;
        bool isNumeric = int.TryParse(newPIN, out result);
        if (newPIN.Length != 4)
        {
            Console.WriteLine("PIN must contain 4 numbers only");
            Thread.Sleep(3000);
            return;
        }
        if (isNumeric)
        {
            Console.WriteLine("PIN succesfully changed");
            History.Add($"{DateTime.Now}:PIN has changed");
            PIN = newPIN;
            Thread.Sleep(3000);
        }
        else
        {
            Console.WriteLine("PIN must contain only a  numbers");
            History.Add($"{DateTime.Now}:PIN change failed");
            Thread.Sleep(3000);
        }
    }
    virtual public void AddCash(float cash)
    {
        Balance += cash;
        Console.WriteLine($"You has succesfully added the {(int)cash} AZN  {(int)((cash - (int)cash) * 100)} qepik\n");
        Console.WriteLine($"Balance:{(int)Balance} AZN  {(int)((Balance - (int)Balance) * 100)} qepik\n");
        History.Add($"{DateTime.Now}:{cash} azn has added to balance");
        Thread.Sleep(5000);
    }
    virtual public void DrawCash(float drawcash)
    {
        if (Balance >= drawcash)
        {
            Balance -= drawcash;
            Console.WriteLine($"You has succesfully draw the {(int)drawcash} AZN  {(int)((drawcash - (int)drawcash) * 100)} qepik\n");
            Console.WriteLine($"Balance:{(int)Balance} AZN  {(int)((Balance - (int)Balance) * 100)} qepik\n");
            History.Add($"{DateTime.Now}:Drawn {drawcash} azn");
            Thread.Sleep(5000);
        }
        else
        {
            Console.WriteLine("Not enough money");
            Thread.Sleep(4000);
        }
    }
    virtual public void ShowMoney()
    {
        Console.WriteLine($"Balance:{(int)Balance} AZN  {(int)((Balance - (int)Balance) * 100)} qepik\n");
        History.Add($"{DateTime.Now}: Balance checked.");

    }
    virtual public void ShowHistory()
    {
        Console.WriteLine("History--------------------");
        foreach (var item in History)
        {
            Console.WriteLine($"{item}\n");
        }
    }


}
public class StepCard : BankCard
{
    public StepCard(string pin) : base(pin)
    {
    }

    protected string? BankCardName { get; set; }
    protected override string? PAN { get; set; }
    public override string? PIN { get; set; }
    protected override string? CVC { get; set; }
    protected override DateTime ExpireDate { get; set; }
    protected override float Balance { get; set; }

    public override List<string?> History { get; set; }
    public override void ChangePIN(string newPIN)
    {
        base.ChangePIN(newPIN);
    }
    public override void AddCash(float cash)
    {
        base.AddCash(cash);
    }
    public override void DrawCash(float drawcash)
    {
        base.DrawCash(drawcash);
    }

    public override void ShowMoney()
    {
        base.ShowMoney();
    }
    public override void ShowHistory()
    {
        base.ShowHistory();
    }
}
public class PetsCard : BankCard
{
    public PetsCard(string pin) : base(pin)
    {
    }

    protected string? BankCardName { get; set; }
    protected override string? PAN { get; set; }
    public override string? PIN { get; set; }
    protected override string? CVC { get; set; }
    protected override DateTime ExpireDate { get; set; }
    protected override float Balance { get; set; }
    public override List<string?> History { get; set; }

    public override void ChangePIN(string newPIN)
    {
        base.ChangePIN(newPIN);
    }
    public override void AddCash(float cash)
    {
        base.AddCash(cash);
    }
    public override void DrawCash(float drawcash)
    {
        base.DrawCash(drawcash);
    }

    public override void ShowMoney()
    {
        base.ShowMoney();
    }
    public override void ShowHistory()
    {
        base.ShowHistory();
    }
}
public class Client
{
    public string? ID { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime Birthday { get; set; }
    public float Salary { get; set; }
    public BankCard BankCard { get; set; }
    public Client(string? name, string? surname, DateTime birthday, float salary, BankCard bankCard)
    {
        ID = GenerateUnigueID();
        Name = name;
        Surname = surname;
        Birthday = birthday;
        Salary = salary;
        BankCard = bankCard;
    }
    public override string ToString()
    {
        return $"ID:{ID}\nName:{Name}\nSurname:{Surname}\nAge:{DateTime.Now.Year - Birthday.Year}\nSalary:{Salary} AZN\nCardName{BankCard.GetType()}";
    }
    private string GenerateUnigueID()
    {
        string? newID = "";
        string? guidString = Guid.NewGuid().ToString();
        for (int i = 0; i < 8; i++)
        {
            newID += guidString[i];
        }
        return newID;
    }
}
abstract public class Bank
{
    public Bank(List<Client> clients)
    {
        Clients = clients;
        BankName = this.GetType().ToString();
    }
    abstract public List<Client> Clients { get; set; }
    abstract public string? BankName { get; set; }
    virtual public void AddClient(Client? newclient)
    {
        Clients.Add(newclient);
    }
    virtual protected void RemoveClient(int index)
    {
        Clients.Remove(Clients[index]);
    }
    virtual protected int? CheckPIN(string? PIN)
    {
        int PRIMANKA;
        bool isNumeric = int.TryParse(PIN, out PRIMANKA);
        if (PIN.Length != 4)
        {
            Console.WriteLine("PIN must contain 4 numbers only try again");
            Thread.Sleep(3000);
            return null;
        }
        if (!isNumeric)
        {
            Console.WriteLine("PIN must contain only a  numbers try again");
            Thread.Sleep(3000);
            return null;
        }
        for (int i = 0; i < Clients.Count; i++)
        {
            if (PIN == Clients[i].BankCard.PIN)
            {
                Console.WriteLine($"Correct,welcome {Clients[i].Name} {Clients[i].Surname}");
                Thread.Sleep(2000);
                return i;
            }
        }
        Console.WriteLine("PIN did not found try next time");
        Thread.Sleep(3000);
        return null;
    }
    virtual protected int? WritePIN()
    {
        Console.Write("Write the PIN(4 number or write 'e' for exit):");
        string? PIN = Console.ReadLine();
        if (PIN == "e" || PIN == "E")
        {
            Console.WriteLine("Program stopped!");
            return -1;
        }
        int? clientCatch = CheckPIN(PIN);
        if (clientCatch != null)
        {
            return clientCatch;
        }
        else
        {
            WritePIN();
        }
        return null;
    }
    virtual protected void ShowClientBalance(int? clientindex)
    {
        Console.Clear();
        Clients[Convert.ToInt32(clientindex)].BankCard.ShowMoney();
        Thread.Sleep(4000);
    }
    virtual protected void AddMoney(int? clientindex)
    {
        Console.Clear();
        Console.Write("Write how many money do you want to add:");
        float cash = float.Parse(Console.ReadLine());
        Clients[Convert.ToInt32(clientindex)].BankCard.AddCash(cash);
    }
    virtual protected void DrawMoney(int? clientindex)
    {
        Console.Clear();
        Console.Write("Write how many do you want to draw\n1:10 azn\n2:20 azn\n3:50 azn\n4:100 azn\nOr write how much do you want(must be higher than 10)\n");
        float cash = float.Parse(Console.ReadLine());
        if (cash == 1) cash = 10;
        if (cash == 1) cash = 20;
        if (cash == 1) cash = 50;
        if (cash == 1) cash = 100;
        Clients[Convert.ToInt32(clientindex)].BankCard.DrawCash(cash);
    }
    virtual protected void ChangePIN(int? clientindex)
    {
        Console.WriteLine("Please write the new PIN(must be 4 numbers only:");
        string? newPIN = Console.ReadLine();
        Clients[Convert.ToInt32(clientindex)].BankCard.ChangePIN(newPIN);
    }
    virtual protected void ShowClientHistory(int? clientIndex)
    {
        Console.Clear();
        Clients[Convert.ToInt32(clientIndex)].BankCard.ShowHistory();
        Thread.Sleep(4000);
    }
    virtual protected void UI(int? clientIndex)
    {
        Console.Clear();
        Console.WriteLine("\n1:Balance\n2:Add money\n3:Draw cash money\n4:Card History\n5:Change PIN code\n6:Exit");
        int key = int.Parse(Console.ReadLine());
        switch (key)
        {
            case 1: ShowClientBalance(clientIndex); break;
            case 2: AddMoney(clientIndex); break;
            case 3: DrawMoney(clientIndex); break;
            case 4: ShowClientHistory(clientIndex); break;
            case 5: ChangePIN(clientIndex); break;
            case 6: return;
        }
        UI(clientIndex);
    }
    virtual public void Bankomat()
    {
        int? clientIndex = WritePIN();
        if (clientIndex == -1) return;
        UI(clientIndex);
        Bankomat();
    }

}
public class StepBank : Bank
{
    public StepBank(List<Client> clients) : base(clients) { }
    public override List<Client> Clients { get; set; }
    public override string? BankName { get; set; }

    public override void AddClient(Client? newclient)
    {
        base.AddClient(newclient);
    }
    //methods
    protected override void AddMoney(int? clientindex)
    {
        base.AddMoney(clientindex);
    }
    public override void Bankomat()
    {
        base.Bankomat();
    }
    protected override int? CheckPIN(string? PIN)
    {
        return base.CheckPIN(PIN);
    }
    protected override void DrawMoney(int? clientindex)
    {
        base.DrawMoney(clientindex);
    }
    protected override void RemoveClient(int index)
    {
        base.RemoveClient(index);
    }
    protected override void ShowClientBalance(int? clientindex)
    {
        base.ShowClientBalance(clientindex);
    }
    protected override void ShowClientHistory(int? clientIndex)
    {
        base.ShowClientHistory(clientIndex);
    }
    protected override void ChangePIN(int? clientindex)
    {
        base.ChangePIN(clientindex);
    }
    protected void ShowClients()
    {
        foreach (var item in Clients)
        {
            Console.WriteLine($"{item}\n");
        }
    }
    protected override void UI(int? clientIndex)
    {
        base.UI(clientIndex);
    }
    protected override int? WritePIN()
    {
        return base.WritePIN();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Client c1 = new Client("Emil", "Tagiyev", new DateTime(1994, 6, 17), 900, new StepCard("1111"));
        Client c2 = new Client("Umid", "Nagiyev", new DateTime(1994, 3, 21), 1500, new StepCard("1212"));
        Client c3 = new Client("Amil", "Qasimov", new DateTime(1994, 1, 30), 2000, new StepCard("1313"));
        Client c4 = new Client("Vasif", "Yusifov", new DateTime(1994, 9, 26), 700, new PetsCard("1414"));
        Client c5 = new Client("NAzim", "Fikretov", new DateTime(1994, 4, 12), 600, new PetsCard("1515"));
        List<Client?>? clients = new List<Client?> { c1, c2, c3, c4, c5 };
        StepBank stepBank = new StepBank(clients);
        stepBank.Bankomat();
    }
}