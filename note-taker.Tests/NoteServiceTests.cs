using Xunit;
using FakeItEasy;
using note_taker;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        private readonly IFileService _fileService;
        private readonly NoteService _noteService;

        public NoteServiceTests()
        {
            _fileService = A.Fake<IFileService>();
            _noteService = new NoteService(_fileService);
        }

        [Fact]
        public void TestMethod1()
        {
            // Test code for method 1
        }

        [Fact]
        public void TestMethod2()
        {
            // Test code for method 2
        }

        // Add more test methods for other methods in the NoteService class
    }
}

