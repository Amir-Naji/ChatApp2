using Helpers;
using System.Text;

namespace HelpersTests
{
    public class ConvertersTests
    {
        private readonly IConverters _converters = new Converters();

        [Test]
        public void StreamToString_NullInput_NullException()
        {
            Assert.ThrowsAsync<NullReferenceException>(() => _converters.StreamToString(null));
        }

        [Test]
        public async Task StreamToString_NewStream_ReturnNull()
        {
            Assert.That(await _converters.StreamToString(new MemoryStream()), Is.EqualTo(null));
        }

        [Test]
        public async Task StreamToString_CorrectStream_ReturnSomeValue()
        {
            Assert.That(
                await _converters.StreamToString(
                    new MemoryStream(
                        _converters.StringMessageToByteArray("Test"))), 
                Is.EqualTo("Test"));
        }

        [Test]
        public async Task SimpleStreamToString_CorrectStream_ReturnCorrectValue()
        {
            Assert.That(
                await _converters.SimpleStreamToString(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes("Test"))), 
                Is.EqualTo("Test"));
        }

        [Test]
        public void SimpleStreamToString_NullInput_NullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _converters.SimpleStreamToString(null));
        }

        [Test]
        public async Task SimpleStreamToString_NewStream_ReturnNull()
        {
            Assert.That(await _converters.SimpleStreamToString(new MemoryStream()), Is.EqualTo(string.Empty));
        }

        [Test]
        public void SimpleStringToStream_CorrectString_CorrectStream()
        {
            var bytes = Encoding.UTF8.GetBytes("Test");
            Assert.That(_converters.SimpleStringToStream("Test"), Is.EqualTo(bytes));
        }

        [Test]
        public void StringToByteArray_CorrectString_ReturnCorrectOutput()
        {
            Assert.That(_converters.StringMessageToByteArray("Test"), Is.EqualTo(new byte[] {4, 0, 0, 0, 84, 101, 115, 116}));
        }

        [Test]
        public void StringToByteArray_NullInput_NullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => _converters.StringMessageToByteArray(null));
        }

        [Test]
        public void StringToByteArray_EmptyString_CorrectByteArray()
        {
            Assert.That(_converters.StringMessageToByteArray(string.Empty), Is.EqualTo(new byte[] { 0, 0, 0, 0 }));
        }
    }
}