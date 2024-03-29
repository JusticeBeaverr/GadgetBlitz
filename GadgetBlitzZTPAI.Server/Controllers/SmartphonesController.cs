﻿using GadgetBlitzZTPAI.Server.Application.Commands;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Application.Queries;
using GadgetBlitzZTPAI.Server.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GadgetBlitzZTPAI.Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("gadgetblitz/api/v{version:apiVersion}/")]
    [ApiController]

    public class SmartphonesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SmartphonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("smartphones")]
        public async Task<ActionResult<List<SmartphoneResponseDTO>>> GetAllSmartphones()
        {
            var query = new GetAllSmartphonesQuery();
            var result = await _mediator.Send(query);

            var smartphones = result.Items;
            return Ok(smartphones);

        }

        [HttpPost]
        public async Task<ActionResult<SmartphoneDTO>> AddSmartphone([FromBody] SmartphoneDTO smartphone)
        {
            try
            {
                var command = new AddSmartphoneCommand(smartphone);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("id")]
        
        public async Task<ActionResult<SmartphoneResponseDTO>> GetSmartphoneById(Guid id)
        {
            var query = new GetSmartphoneByIDQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("smartphoneid"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSmartphoneById(Guid id)
        {
            var command = new DeleteSmartphoneCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("reviews"), Authorize(Roles = "User")]
        public async Task<ActionResult> AddReview([FromBody] AddReviewDTO reviewDto)
        {
            try
            {
                var command = new AddReviewCommand(reviewDto);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
