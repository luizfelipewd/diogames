using System;

namespace diogames.Exceptions
{
    public class GameDoesNotExistsException : Exception
    {
        public GameDoesNotExistsException() : base("Jogo não cadastrado")
        {
        }
    }
}