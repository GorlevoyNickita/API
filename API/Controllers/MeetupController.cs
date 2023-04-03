using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using API.models;
using Microsoft.EntityFrameworkCore;

namespace API.Controller
{
    [Route("api/meetup")]
    public class MeetupController : ControllerBase
    {
        private readonly MeetUpContext _meetUpContext;
        private readonly IMapper _mapper;
        public MeetupController(MeetUpContext meetUpContext, IMapper mapper     )
        {
            _meetUpContext = meetUpContext;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<MeetupDetailsDto>> Get()
        {
            var meetups = _meetUpContext.MeetUps.ToList();
            var meetupDtos = _mapper.Map<List<MeetupDetailsDto>>(meetups);
            return Ok(meetupDtos);
        }
        [HttpGet("{name}")]
        public ActionResult<MeetupDetailsDto> Get(string name)
        {
            var meetup = _meetUpContext.MeetUps
                .Include(m => m.Location)
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());
            if(meetup == null)
            {
                return NotFound();
            }
            var meetupDto = _mapper.Map<MeetupDetailsDto>(meetup);
            return Ok(meetupDto);
        }
        [HttpPost]
        public ActionResult Post([FromBody]MeetupDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var meetup = _mapper.Map<MeetUp>(model);
            _meetUpContext.MeetUps.Add(meetup);
            _meetUpContext.SaveChanges();

            var key = meetup.Name.Replace(" ", "-").ToLower();
            return Created("api/meetup/" + key,null);
        }
        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] MeetupDto model)
        {
            var meetup = _meetUpContext.MeetUps
               .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());
            if (meetup == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            meetup.Name = model.Name;
            meetup.Organizer = model.Organizer;
            meetup.Date = model.Date;
            meetup.IsPrivate = model.IsPrivate;

            _meetUpContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{name}")]
        public ActionResult Delete(string  name)
        {
            var meetup = _meetUpContext.MeetUps
                          .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());
            if (meetup == null)
            {
                return NotFound();
            }
            _meetUpContext.Remove(meetup);
            _meetUpContext.SaveChanges();

            return NoContent();
        }
    }
}
