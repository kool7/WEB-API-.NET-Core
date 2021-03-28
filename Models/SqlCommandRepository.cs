using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SqlCommandRepository : ICommandRepository
    {
        private readonly CommandDbContext _context;

        public SqlCommandRepository(CommandDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);
        }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Command> GetCommands()
        {
            return _context.Commands.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            
        }
    }
}
