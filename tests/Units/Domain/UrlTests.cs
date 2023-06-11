using Domain.Entities;

namespace Units.Domain;

public sealed class UrlTests
{
    [Fact]
    public void ShortUrlShoulBeCreated()
    {
        //arrange
        Url url = new();

        //Act
        url.CreateShortUrl();

        //Assert
        Assert.NotNull(url.ShortUrl);
    }

    [Theory]
    [InlineData("http://test", "test")]
    [InlineData("https://test", "test")]
    [InlineData("test", "test")]
    public void ShortUrlShouldBeAdded(string uri, string expected)
    {
        //arrange
        Url url = new();

        //Act
        url.AddShortUrl(uri);

        //Assert
        Assert.Equal(expected, url.ShortUrl);
    }
}