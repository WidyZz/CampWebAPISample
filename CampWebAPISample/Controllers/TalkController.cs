using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CampWebAPISample.Controllers {
    [Route("api/camp/{moniker}/[controller]")]
    [ApiController]
    public class TalkController : ControllerBase {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalkController(ICampRepository repository, IMapper mapper) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker) {
            try {
                var talks = await _repository.GetTalksByMonikerAsync(moniker, true);
                return _mapper.Map<TalkModel[]>(talks);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> GetById(string moniker, int id) {
            try {
                var concreteTalk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                if (concreteTalk == null) return NotFound($"Moniker \"{moniker}\" has no talk with ID of ({id})");
                return _mapper.Map<TalkModel>(concreteTalk);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot get Talks");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel talk) {
            try {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null) {
                    return BadRequest("Talk does not exist exists");
                }


                // Create a new Camp
                var talks = _mapper.Map<Talk>(talk);
                talks.Camp = camp;

                if (talk.Speaker == null) return BadRequest("Speaker ID is required");
                var speaker = await _repository.GetSpeakerAsync(talk.Speaker.SpeakerId);
                if (speaker == null) return BadRequest("Speaker could not be found");
                talks.Speaker = speaker;

                _repository.Add(talks);
                if (await _repository.SaveChangesAsync()) {
                    return Created($"/api/camp/{moniker}/talks/{talks.TalkId}", _mapper.Map<TalkModel>(talk));
                }
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error");
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model) {
            try {
                var oldTalk = await _repository.GetTalkByMonikerAsync(moniker, id, true);

                if (oldTalk == null) return NotFound($"Talk in moniker \"{moniker}\" with ID of ({id}) not found.");
                _mapper.Map(model, oldTalk);

                if (model.Speaker != null)
                {
                    var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null)
                        oldTalk.Speaker = speaker;
                }


                if (await _repository.SaveChangesAsync()) {
                    return _mapper.Map<TalkModel>(oldTalk);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, false);
                if (talk == null) return NotFound($"Talk with ID of {id}");

                _repository.Delete(talk);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }
    }
}
