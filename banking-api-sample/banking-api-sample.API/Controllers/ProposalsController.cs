using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApiSample.Application.DTOs;
using BankingApiSample.Application.Interfaces;
using BankingApiSample.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BankingApiSample.API.Controllers
{
    [ApiController]
    [Route("proposals")]
    public class ProposalsController : ControllerBase
    {
        private readonly IProposalService _service;

        public ProposalsController(IProposalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ProposalDto>> Create([FromBody] CreateProposalDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProposalDto>> GetById(Guid id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProposalDto>>> List([FromQuery] ProposalStatus? status)
        {
            var list = await _service.ListAsync(status);
            return Ok(list);
        }

        [HttpPost("{id}/review")]
        public async Task<IActionResult> Review(Guid id)
        {
            await _service.ReviewAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _service.ApproveAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            await _service.RejectAsync(id);
            return NoContent();
        }
    }
}
