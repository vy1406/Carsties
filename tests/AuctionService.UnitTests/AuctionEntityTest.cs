namespace AuctionService.UnitTests;

public class AuctionEntityTest
{
    [Fact]
    public void HasReservePrice_ReservePriceGgZero_True() // methodname + scenario + output
    {
        var auction = new Auction { ReservePrice = 1 };

        var result = auction.HasReservePrice();

        Assert.True(result);
    }
    [Fact]
    public void HasReservePrice_ReservePriceIsZero_False() // methodname + scenario + output
    {
        var auction = new Auction { ReservePrice = 0 };

        var result = auction.HasReservePrice();

        Assert.False(result);
    }
}
