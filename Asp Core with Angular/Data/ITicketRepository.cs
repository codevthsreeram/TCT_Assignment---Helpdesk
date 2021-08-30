using Asp_Core_with_Angular.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Core_with_Angular.Data
{
    public interface ITicketRepository
    {
        Task<List<TicketDetail>> GetAll();
        Task<TicketDetail> GetById(Guid id);
        Task<TicketDetail> Create(TicketDetail ticketDetail);
        Task<bool> Update(Guid id, TicketDetail ticketDetail);
        Task<bool> Delete(Guid id);
    }
}
