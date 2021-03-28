using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandController(ICommandRepository commandRepository,
                                 IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _commandRepository.GetCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{Id}", Name = "GetCommandsById")]
        public ActionResult<CommandReadDto> GetCommandsById(int Id)
        {
            var commandItem = _commandRepository.GetCommandById(Id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _commandRepository.CreateCommand(commandModel);
            _commandRepository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandsById), new { Id = commandReadDto.Id}, commandReadDto);
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateCommand(int Id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _commandRepository.GetCommandById(Id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _commandRepository.UpdateCommand(commandModelFromRepo);
            _commandRepository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public ActionResult PartialCommandUpdate(int Id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _commandRepository.GetCommandById(Id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);
            _commandRepository.UpdateCommand(commandModelFromRepo);
            _commandRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteCommand(int Id)
        {
            var commandModelFromRepo = _commandRepository.GetCommandById(Id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _commandRepository.DeleteCommand(commandModelFromRepo);
            _commandRepository.SaveChanges();
            return NoContent();
        }
    }
}