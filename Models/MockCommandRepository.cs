using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class MockCommandRepository : ICommandRepository
    {
        private ObservableCollection<Command> _commandsList;
        public MockCommandRepository()
        {
            _commandsList = new ObservableCollection<Command>()
            {
                new Command{Id = 0, HowTo="Boil an egg", Line="Boil water", Platform="Kettle & Pan"},
                new Command{Id = 1, HowTo="Cut bread", Line="Get a knife", Platform="Knife and Chopping Board"},
                new Command{Id = 2, HowTo="Make a cup of tea", Line="Place teabag in cup", Platform="Kettle & Cup"}
            };
        }

        public void CreateCommand(Command cmd)
        {
            cmd.Id = _commandsList.Max(e => e.Id) + 1;
            _commandsList.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            _commandsList.Remove(cmd);
        }

        public Command GetCommandById(int Id)
        {
            return _commandsList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Command> GetCommands()
        {
            return _commandsList;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            Command command = _commandsList.FirstOrDefault(e => e.Id == cmd.Id);

            if (command != null)
            {
                command.HowTo = cmd.HowTo;
                command.Line = cmd.Line;
                command.Platform = cmd.Platform;
            }
        }
    }
}
