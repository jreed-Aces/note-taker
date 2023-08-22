using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using note_taker.Services;
using note_taker.Interfaces;

namespace note_taker.Tests
{
    [TestClass]
    public class NoteServiceTests
    {
        private INoteService _noteService;
        private IFileService _fileService;

        [TestInitialize]
        public void Initialize()
        {
            _fileService = A.Fake<IFileService>();
            _noteService = new NoteService(_fileService);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            // Set up necessary behavior for the mock _fileService instance

            // Act
            // Call the corresponding method on _noteService

            // Assert
            // Assert the expected results
        }

        // Add more test methods for each method in the NoteService class
    }
}

