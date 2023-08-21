using Xunit;
using FakeItEasy;
using note_taker.Services;
using System.Collections.Generic;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        private readonly NoteService _noteService;
        private readonly IFileService _fileService;

        public NoteServiceTests()
        {
            _fileService = A.Fake<IFileService>();
            _noteService = new NoteService(_fileService);
        }

        [Fact]
        public void AddNote_ShouldReturnTrue_WhenNoteIsAdded()
        {
            // Arrange
            string note = "Test note";
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns("[]");

            // Act
            bool result = _noteService.AddNote(note);

            // Assert
            Assert.True(result);
            A.CallTo(() => _fileService.WriteAllText("notes.json", A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_ShouldReturnListOfNotes_WhenCalled()
        {
            // Arrange
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns("[{\"Id\":1,\"Content\":\"Test note\",\"NoteStatus\":0,\"Timestamp\":\"2022-01-01T00:00:00\"}]");

            // Act
            List<Note> notes = _noteService.PrintNotes();

            // Assert
            Assert.Single(notes);
        }

        [Fact]
        public void UpdateNoteStatus_ShouldReturnTrue_WhenNoteExists()
        {
            // Arrange
            int id = 1;
            Note.Status status = Note.Status.Closed;
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns("[{\"Id\":1,\"Content\":\"Test note\",\"NoteStatus\":0,\"Timestamp\":\"2022-01-01T00:00:00\"}]");

            // Act
            bool result = _noteService.UpdateNoteStatus(id, status);

            // Assert
            Assert.True(result);
            A.CallTo(() => _fileService.WriteAllText("notes.json", A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}

