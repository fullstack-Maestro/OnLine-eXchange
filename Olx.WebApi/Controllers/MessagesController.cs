using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.Messages;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageViewDto>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageViewDto>> GetMessageById(long id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound("Message not found.");
            }

            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewDto>> AddMessage(MessageCreateDto messageCreateDto)
        {
            var addedMessage = await _messageService.CreateAsync(messageCreateDto);
            return Ok(addedMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MessageViewDto>> UpdateMessage(long id, MessageUpdateDto messageUpdateDto)
        {
            var updatedMessage = await _messageService.UpdateAsync(id, messageUpdateDto);
            if (updatedMessage == null)
            {
                return NotFound("Message not found.");
            }

            return Ok(updatedMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(long id)
        {
            var isDeleted = await _messageService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("Message not found.");
            }

            return NoContent();
        }
    }
}