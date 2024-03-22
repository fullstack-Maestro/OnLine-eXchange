using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Messages;


namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IRepository<Message> _messageRepository;

    public MessagesController(IRepository<Message> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    [HttpGet]
    public ActionResult<List<MessageViewDto>> GetAllMessages()
    {
        var messages = _messageRepository.SelectAllAsEnumerable()
            .Where(message => !message.IsDeleted)
            .ToList();
        var messageViews = messages.Select(m => new MessageViewDto
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            PostId = m.PostId,
            Content = m.Content,
            SendDate = m.SendDate
        }).ToList();

        return Ok(messageViews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MessageViewDto>> GetMessageById(long id)
    {
        var message = await _messageRepository.SelectByIdAsync(id);
        if (message == null)
        {
            return NotFound("Message not found.");
        }

        var messageView = new MessageViewDto
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            PostId = message.PostId,
            Content = message.Content,
            SendDate = message.SendDate
        };

        return Ok(messageView);
    }

    [HttpPost]
    public async Task<ActionResult<MessageViewDto>> AddMessage(MessageCreateDto messageCreateDto)
    {
        var message = new Message
        {
            SenderId = messageCreateDto.SenderId,
            ReceiverId = messageCreateDto.ReceiverId,
            PostId = messageCreateDto.PostId,
            Content = messageCreateDto.Content,
            SendDate = DateTime.UtcNow
        };

        var addedMessage = await _messageRepository.InsertAsync(message);
        await _messageRepository.SaveAsync();

        var messageView = new MessageViewDto
        {
            Id = addedMessage.Id,
            SenderId = addedMessage.SenderId,
            ReceiverId = addedMessage.ReceiverId,
            PostId = addedMessage.PostId,
            Content = addedMessage.Content,
            SendDate = addedMessage.SendDate
        };

        return Ok(messageView);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageViewDto>> UpdateMessage(long id, MessageUpdateDto messageUpdateDto)
    {
        var existingMessage = await _messageRepository.SelectByIdAsync(id);
        if (existingMessage == null)
        {
            return NotFound("Message not found.");
        }

        existingMessage.SenderId = messageUpdateDto.SenderId;
        existingMessage.ReceiverId = messageUpdateDto.ReceiverId;
        existingMessage.PostId = messageUpdateDto.PostId;
        existingMessage.Content = messageUpdateDto.Content;

        var updatedMessage = await _messageRepository.UpdateAsync(existingMessage);
        await _messageRepository.SaveAsync();

        var messageView = new MessageViewDto
        {
            Id = updatedMessage.Id,
            SenderId = updatedMessage.SenderId,
            ReceiverId = updatedMessage.ReceiverId,
            PostId = updatedMessage.PostId,
            Content = updatedMessage.Content,
            SendDate = updatedMessage.SendDate
        };

        return Ok(messageView);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(long id)
    {
        var existingMessage = await _messageRepository.SelectByIdAsync(id);
        if (existingMessage == null)
        {
            return NotFound("Message not found.");
        }

        await _messageRepository.DeleteAsync(existingMessage);
        await _messageRepository.SaveAsync();

        return NoContent();
    }
}