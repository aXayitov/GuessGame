﻿namespace GuessGame1.Entity.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
