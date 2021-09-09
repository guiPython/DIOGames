using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIOGames.Exceptions
{
    public class GameNotExistsException : Exception
    {
        public GameNotExistsException() : base("Game isn't inserted") { }
    }
}
