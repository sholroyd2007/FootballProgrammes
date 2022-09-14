using FootballProgrammes.Data;
using FootballProgrammes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballProgrammes.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesByUserId(string id);
        Task<IEnumerable<Message>> GetUnreadMessagesByUserId(string id);
        Task<Message> GetMessageById(int id);
        Task<Message> CreateMessage(Message message);
        Task DeleteMessage(int id);
        Task MarkMessageAsRead(int id);

    }
    public class MessageService : IMessageService
    {
        public MessageService(ApplicationDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public ApplicationDbContext DatabaseContext { get; }


        public async Task<Message> CreateMessage(Message message)
        {
            DatabaseContext.Add(message);
            await DatabaseContext.SaveChangesAsync();
            return message;
        }

        public async Task DeleteMessage(int id)
        {
            var message = await GetMessageById(id);
            message.isDeleted = true;
            DatabaseContext.Update(message);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            var message = await DatabaseContext.Messages.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return message;
        }

        public async Task<IEnumerable<Message>> GetMessagesByUserId(string id)
        {
            var messages = await DatabaseContext.Messages.AsNoTracking().Where(e=>e.ToUserId == id && !e.isDeleted).ToListAsync();
            return messages;
        }

        public async Task<IEnumerable<Message>> GetUnreadMessagesByUserId(string id)
        {
            var userMessages = await GetMessagesByUserId(id);
            return userMessages.Where(e=>!e.Read);
        }

        public async Task MarkMessageAsRead(int id)
        {
            var message = await GetMessageById(id);
            message.Read = true;
            message.ReadDate = DateTime.UtcNow;
            DatabaseContext.Update(message);
            await DatabaseContext.SaveChangesAsync();
        }
    }
}
