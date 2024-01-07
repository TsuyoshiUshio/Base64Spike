using System.Text;

namespace Base64Spike.Tests
{
    public class Base64UtilsTests
    {
        [Theory]
        [InlineData("ABCDEFG")]
        [InlineData("Hello world")]
        public void Encode(string text)
        {
            var actual = Base64Utils.Encode(text);
            var expected = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            Assert.Equal(expected, actual);
        }
    }
}