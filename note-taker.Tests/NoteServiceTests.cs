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
            Assert.Equal(note, _noteService.PrintNotes().Last().Content);
        }

        [Fact]
        public void PrintNotes_ShouldReturnCorrectNotes()
        {
            // Arrange
            List<Note> expectedNotes = new List<Note> { new Note(1, "Test note") };
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns(JsonConvert.SerializeObject(expectedNotes));
        
            // Act
            List<Note> actualNotes = _noteService.PrintNotes();
        
            // Assert
            Assert.Equal(expectedNotes, actualNotes);
        }
        
        [Fact]
        public void UpdateNoteStatus_ShouldUpdateStatusCorrectly()
        {
            // Arrange
            Note expectedNote = new Note(1, "Test note");
            expectedNote.NoteStatus = Note.Status.Closed;
            A.CallTo(() => _fileService.Exists("notes.json")).Returns(true);
            A.CallTo(() => _fileService.ReadAllText("notes.json")).Returns(JsonConvert.SerializeObject(new List<Note> { expectedNote }));
        
            // Act
            bool result = _noteService.UpdateNoteStatus(expectedNote.Id, Note.Status.Closed);
        
            // Assert
            Assert.True(result);
            Assert.Equal(expectedNote.NoteStatus, _noteService.PrintNotes().Find(n => n.Id == expectedNote.Id).NoteStatus);
        }
    }
}

