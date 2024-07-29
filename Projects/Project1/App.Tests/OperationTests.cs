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

        string a = "0082/07/22";
        string b = "0082/07/22";

        int result = Operation.GetNumOfDays(a, b);

        Assert.Equal(0, result);
    }

    [Fact]
    public void GetNumOfDaysDoesntCareAboutOrder()
    {
        string a = "0082/07/23";
        string b = "0082/07/30";

        int T1 = Operation.GetNumOfDays(a, b);
        int T2 = Operation.GetNumOfDays(b, a);

        bool test = (T1 == T2);

        Assert.True(test);
    }

    [Theory]
    [InlineData("0082/07/30", "0082/07/23", 7)]
    [InlineData("0082/07/23", "0082/07/30", 7)]
    [InlineData("0083/07/23", "0082/07/23", 360)]
    [InlineData("0082/08/23", "0082/07/23", 30)]
    [InlineData("0083/08/23", "0082/07/23", 390)]
    [InlineData("0083/08/30", "0082/07/23", 397)]
    [InlineData("0082/08/14", "0082/07/22", 22)]
    public void GetNumOfDaysReturnsCorrectNumber(string a, string b, int expected){
        int result = Operation.GetNumOfDays(a,b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsZeroOnTen(){
        Workday wd = new Workday("0082/07/22", 0, 0, 300, 0, 0, "");
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, 10);
        Assert.Equal(wd, result);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue1(){
        Workday wd = new Workday("0082/07/22", 0, 0, 100, 0, 0, "");
        int roll = 12;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(20, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue2(){
        Workday wd = new Workday("0082/07/22", 0, 0, 100, 0, 0, "");
        int roll = 8;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(-20, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeReturnsCorrectValue3(){
        Workday wd = new Workday("0082/07/22", 0, 0, 100, 0, 0, "");
        int roll = 19;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        Assert.Equal(90, result.EndOfWeekChange);
    }

    [Fact]
    public void GenerateRandomEndOfWeekChangeNotesCorrectly(){
        Workday wd = new Workday("0082/07/22", 0, 0, 100, 0, 0, "");
        int roll = 20;
        Workday result = Operation.GenerateRandomEndOfWeekChange(wd, roll);

        bool test = ((result.Notes.Contains("FLAG: Good")) && (result.EndOfWeekChange == 0));

        Assert.True(test);
    }

    [Fact]
    public void AdvanceDayReturnsCorrectOnWeekRollover()
    {
        Workday wd = new Workday("0082/7/27", 7, 0, 0, 0, 0, "");
        Workday resultWD = Operation.AdvanceDay(wd);

        Workday expectedWD = new Workday("0082/7/28", 1, 0, 0, 0, 0, "");

        (string, int) result = (resultWD.Date, resultWD.DayOfWeek);
        (string, int) expected = (resultWD.Date, resultWD.DayOfWeek);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDateReturnsCorrectOnMonthRollover()
    {
        Workday wd = new Workday("0082/7/30", 0, 0, 0, 0, 0, "");
        Workday resultWD = Operation.AdvanceDate(wd);

        Workday expectedWD = new("0082/8/1", 0, 0, 0, 0, 0, "");

        string result = resultWD.Date;
        string expected = expectedWD.Date;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDateReturnsCorrectOnYearRollover()
    {
        Workday wd = new Workday("0082/12/30", 7, 0, 0, 0, 0, "");
        Workday resultWD = Operation.AdvanceDate(wd);

        Workday expectedWD = new("0083/1/1", 1, 0, 0, 0, 0, "");

        string result = resultWD.Date;
        string expected = expectedWD.Date;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AdvanceDayActuallyAdvancesDay()
    {
        Workday wd = new Workday("0082/7/22", 2, 154421, 300, 0, 0, "");
        Workday resultWD = Operation.AdvanceDay(wd);

        Workday expectedWD = new("0082/7/23", 3, 154421, 600, 0, 0, "");

        (string, int, int, int, int) result = (resultWD.Date, resultWD.DayOfWeek, resultWD.CurrentWeekBanked, resultWD.EndOfWeekChange, resultWD.Interest);
        (string, int, int, int, int) expected = (expectedWD.Date, expectedWD.DayOfWeek, expectedWD.CurrentWeekBanked, expectedWD.EndOfWeekChange, expectedWD.Interest);

        Assert.Equal(expected, result);
    }
}