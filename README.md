## 10MinuteMail.net API client 
  [![Build status](https://ci.appveyor.com/api/projects/status/v4l997q4qyq88ily?svg=true)](https://ci.appveyor.com/project/unickq/10minutemail)
  [![Test status](http://teststatusbadge.azurewebsites.net/api/status/unickq/10minutemail)](https://ci.appveyor.com/project/unickq/10minutemail)
  [![NuGet](http://flauschig.ch/nubadge.php?id=10minuteMail.net)](https://www.nuget.org/packages/10minuteMail.net)


#### Code example:
```cs
public class Example
{
    public void Sync()
    {
        var t = new TenMinuteMailNet();
        Console.WriteLine(t.EmailAddress); //Your current email address
        Console.WriteLine(t.LastEmail); //Last email in inbox
        t.GenerateNewEmailAddress(); //Generate new inbox
        Console.WriteLine(t.SecondsLeft); //Seconds left to use email
        t.Reset10Minutes(); //Reset email - add 10 minutes
        t.Reset100Minutes(); //Reset email - add 100 minutes
    }

    public async Task Async()
    {
        var t = new TenMinuteMailNet();
        Console.WriteLine(await t.GetEmailAddressAsync()); //Your current email address
        Console.WriteLine(await t.GetLastEmailAsync()); //Last email in inbox
        await t.GenerateNewEmailAddressAsync(); //Generate new inbox
        Console.WriteLine(await t.GetSecondsLeftAsync()); //Seconds left to use email
        await t.Reset10MinutesAsync(); //Reset email - add 10 minutes
        await t.Reset100MinutesAsync(); //Reset email - add 100 minutes
    }
}
```
