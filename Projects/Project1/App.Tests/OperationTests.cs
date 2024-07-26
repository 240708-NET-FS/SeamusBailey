using App;
using App.Entities;
using App.Service;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace App.Tests;

public class OperationTests 
{
    [Fact]
    public void GetNumOfDaysShouldReturnZeroWhenGivenSameDays()
    {
        //Operation ops = new Operation(); Haha, it's a static function

        string a = "07/22/0082";
        string b = "07/22/0082";

        int result = Operation.GetNumOfDays(a, b);

        Assert.Equal(0, result);
    }

    [Fact]
    public void GetNumOfDaysDoesntCareAboutOrder()
    {
        string a = "07/23/0082";
        string b = "07/30/0082";

        int T1 = Operation.GetNumOfDays(a, b);
        int T2 = Operation.GetNumOfDays(b, a);

        bool test = (T1 == T2);

        Assert.True(test);
    }

    [Theory]
    [InlineData("07/30/0082", "07/23/0082", 7)]
    [InlineData("07/23/0082", "07/30/0082", 7)]
    [InlineData("07/23/0083", "07/23/0082", 360)]
    [InlineData("08/23/0082", "07/23/0082", 30)]
    [InlineData("08/23/0083", "07/23/0082", 390)]
    [InlineData("08/30/0083", "07/23/0082", 397)]
    [InlineData("08/14/0082", "07/22/0082", 22)]
    public void GetNumOfDaysReturnsCorrectNumber(string a, string b, int expected){
        int result = Operation.GetNumOfDays(a,b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsZeroOnTen(){
        Workday wd = new Workday(){Date= "07/22/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=300, EndOfWeekChange=0, Interest=0, Notes=""};
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, 10);
        Assert.Equal(wd, result);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue1(){
        Workday wd = new Workday(){Date= "07/22/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=100, EndOfWeekChange=0, Interest=0, Notes=""};
        int roll = 12;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(20, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue2(){
        Workday wd = new Workday(){Date= "07/22/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=100, EndOfWeekChange=0, Interest=0, Notes=""};
        int roll = 8;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(-20, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue3(){
        Workday wd = new Workday(){Date= "07/22/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=100, EndOfWeekChange=0, Interest=0, Notes=""};
        int roll = 19;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(90, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeNotesCorrectly(){
        Workday wd = new Workday(){Date= "07/22/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=100, EndOfWeekChange=0, Interest=0, Notes=""};
        int roll = 20;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        bool test = ((result.Notes.Contains("FLAG: Good")) && (result.EndOfWeekChange == 0));

        Assert.True(test);
    }

    [Fact]
    public void AdvanceDayReturnsCorrectOnWeekRollover()
    {
        Workday wd = new Workday(){Date= "7/27/0082", DayOfWeek=7, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};
        Workday resultWD = Operation.AdvanceDay(wd);

        Workday expectedWD = new(){Date= "7/28/0082", DayOfWeek=1, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};

        (string, int) result = (resultWD.Date, resultWD.DayOfWeek);
        (string, int) expected = (resultWD.Date, resultWD.DayOfWeek);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDateReturnsCorrectOnMonthRollover()
    {
        Workday wd = new Workday(){Date= "7/30/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};
        Workday resultWD = Operation.AdvanceDate(wd);

        Workday expectedWD = new(){Date= "8/1/0082", DayOfWeek=0, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};

        string result = resultWD.Date;
        string expected = expectedWD.Date;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDateReturnsCorrectOnYearRollover()
    {
        Workday wd = new Workday(){Date= "12/30/0082", DayOfWeek=7, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};
        Workday resultWD = Operation.AdvanceDate(wd);

        Workday expectedWD = new(){Date= "1/1/0083", DayOfWeek=1, Banked=0, CurrentWeekBanked=0, EndOfWeekChange=0, Interest=0, Notes=""};

        string result = resultWD.Date;
        string expected = expectedWD.Date;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDayActuallyAdvancesDay()
    {
        Workday wd = new Workday(){Date= "7/22/0082", DayOfWeek=2, Banked=154421, CurrentWeekBanked=300, EndOfWeekChange=0, Interest=0, Notes=""};
        Workday resultWD = Operation.AdvanceDay(wd);

        Workday expectedWD = new(){Date= "7/23/0082", DayOfWeek=3, Banked=154421, CurrentWeekBanked=600, EndOfWeekChange=0, Interest=0, Notes=""};

        (string, int, int, int, int) result = (resultWD.Date, resultWD.DayOfWeek, resultWD.CurrentWeekBanked, resultWD.EndOfWeekChange, resultWD.Interest);
        (string, int, int, int, int) expected = (expectedWD.Date, expectedWD.DayOfWeek, expectedWD.CurrentWeekBanked, expectedWD.EndOfWeekChange, expectedWD.Interest);

        Assert.Equal(expected, result);
    }
}