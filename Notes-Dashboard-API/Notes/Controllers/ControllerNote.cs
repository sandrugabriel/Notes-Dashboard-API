using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes_Dashboard_API.Notes.Controllers.interfaces;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Services.interfaces;
using Notes_Dashboard_API.System.Exceptions;

namespace Notes_Dashboard_API.Notes.Controllers
{
    public class ControllerNote : ControllerAPINote
    {
        INoteQueryService _query;
        INoteCommandService _command;

        public ControllerNote(INoteQueryService query, INoteCommandService command)
        {
            _query = query;
            _command = command;
        }

        [Authorize]
        public override async Task<ActionResult<List<NoteResponse>>> GetAll()
        {
            try
            {
                var notes = await _query.GetAllAsync();
                return Ok(notes);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<NoteResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var notes = await _query.GetByIdAsync(id);
                return Ok(notes);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<NoteResponse>> GetByTitle([FromQuery] string title)
        {
            try
            {
                var notes = await _query.GetByTitleAsync(title);
                return Ok(notes);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        ////[Authorize]
        //public override async Task<ActionResult<NoteResponse>> CreateNote([FromBody] CreateNoteRequest createRequestNote)
        //{
        //    try
        //    {
        //        var note = await _command.CreateNote(createRequestNote);
        //        return Ok(note);
        //    }
        //    catch (InvalidName ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (InvalidDate ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (InvalidDescription ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        ////[Authorize]
        //public override async Task<ActionResult<NoteResponse>> UpdateNote([FromQuery] int id, [FromBody] UpdateNoteRequest updateRequest)
        //{
        //    try
        //    {
        //        var note = await _command.UpdateNote(id, updateRequest);
        //        return Ok(note);
        //    }
        //    catch (ItemDoesNotExist ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidName ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch(InvalidDate ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (InvalidDescription ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        ////[Authorize]
        //public override async Task<ActionResult<NoteResponse>> DeleteNote([FromQuery] int id)
        //{
        //    try
        //    {
        //        var note = await _command.DeleteNote(id);
        //        return Ok(note);
        //    }
        //    catch (ItemDoesNotExist ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}


    }
}
