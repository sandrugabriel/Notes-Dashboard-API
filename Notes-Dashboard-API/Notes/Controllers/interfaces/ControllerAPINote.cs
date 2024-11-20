using Microsoft.AspNetCore.Mvc;
using Notes_Dashboard_API.Notes.Dto;

namespace Notes_Dashboard_API.Notes.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPINote : ControllerBase
    {


        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<NoteResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<NoteResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(NoteResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<NoteResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByTitle")]
        [ProducesResponseType(statusCode: 200, type: typeof(NoteResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<NoteResponse>> GetByTitle([FromQuery] string title);

        //[HttpPost("CreateNote")]
        //[ProducesResponseType(statusCode: 201, type: typeof(NoteResponse))]
        //[ProducesResponseType(statusCode: 400, type: typeof(string))]
        //public abstract Task<ActionResult<NoteResponse>> CreateNote([FromBody] CreateNoteRequest createRequestNote);

        //[HttpPut("UpdateNote")]
        //[ProducesResponseType(statusCode: 200, type: typeof(NoteResponse))]
        //[ProducesResponseType(statusCode: 400, type: typeof(string))]
        //[ProducesResponseType(statusCode: 404, type: typeof(string))]
        //public abstract Task<ActionResult<NoteResponse>> UpdateNote([FromQuery] int id, [FromBody] UpdateNoteRequest updateRequest);

        //[HttpDelete("DeleteNote")]
        //[ProducesResponseType(statusCode: 200, type: typeof(NoteResponse))]
        //[ProducesResponseType(statusCode: 404, type: typeof(string))]
        //public abstract Task<ActionResult<NoteResponse>> DeleteNote([FromQuery] int id);

    }
}
