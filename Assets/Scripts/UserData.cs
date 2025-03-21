using System;

[Serializable]
public class UserData
{
    public string userName;
    public int cash;
    public int balance;

    // »ý¼ºÀÚ
    public UserData(string name, int cash, int balance)
    {
        this.userName = name;
        this.cash = cash;
        this.balance = balance;
    }
}
