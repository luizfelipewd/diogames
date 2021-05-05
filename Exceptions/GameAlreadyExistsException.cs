using System;

namespace diogames.Exceptions
{
    public class GameAlreadyExistsException : Exception
    {
        public GameAlreadyExistsException() : base("Este jogo já está cadastrado")
        {
        }
    }
}