using FakeItEasy;
using Xunit;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var fileService = A.Fake<IFileService>();
            var noteService = new NoteService(fileService);

            // Act
            // TODO: Add test logic

            // Assert
            // TODO: Add assertions
        }

        // TODO: Add test methods for other methods in the NoteService class
    }
}

