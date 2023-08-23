using Xunit;
using FakeItEasy;
using note_taker.Services;
using System.Collections.Generic;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        private readonly INoteService _noteService;
        private readonly IFileService _fileService;

        public NoteServiceTests()
        {
            _fileService = A.Fake<IFileService>();
            _noteService = new NoteService(_fileService);
        }

        [Fact]
        public void AddNote_ShouldAddNote()
        {
            // Arrange
            string note = "Test note";
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns("[]");

            // Act
            bool result = _noteService.AddNote(note);

            // Assert
            Assert.True(result);
            A.CallTo(() => _fileService.Exists("notes.json")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.ReadAllText("notes.json")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        // Add more tests for the other methods in the NoteService.cs class
    }
}

