using Xunit;
using FakeItEasy;
using note_taker.Services;

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
            A.CallTo(() => _fileService.WriteAllText("notes.json", A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_ShouldReturnNotes()
        {
            // Arrange
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns("[]");

            // Act
            var notes = _noteService.PrintNotes();

            // Assert
            Assert.NotNull(notes);
        }

        [Fact]
        public void UpdateNoteStatus_ShouldUpdateNoteStatus()
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

