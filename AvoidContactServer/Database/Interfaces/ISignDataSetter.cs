﻿using AvoidContactCommon.Sign;

namespace AvoidContactServer.Database.Interfaces
{
    public interface ISignDataSetter
    {
        public void AddPlayer(SignedPlayerInfo signUpModel);
    }
}