using Xunit;
using FakeItEasy;
using note_taker.Services;
using System.Collections.Generic;

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
        public void AddNote_ShouldReturnTrue_WhenNoteIsAdded()
        {
            // Arrange
            string note = "Test note";

            // Act
            bool result = _noteService.AddNote(note);

            // Assert
            Assert.True(result);
            A.CallTo(() => _fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_ShouldReturnListOfNotes_WhenCalled()
        {
            // Arrange
            List<Note> expectedNotes = new List<Note>();

            // Act
            List<Note> actualNotes = _noteService.PrintNotes();

            // Assert
            Assert.Equal(expectedNotes, actualNotes);
            A.CallTo(() => _fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateNoteStatus_ShouldReturnTrue_WhenNoteStatusIsUpdated()
        {
            // Arrange
            int id = 1;
            Note.Status status = Note.Status.Closed;

            // Act
            bool result = _noteService.UpdateNoteStatus(id, status);

            // Assert
            Assert.True(result);
            A.CallTo(() => _fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}

