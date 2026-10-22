// <copyright file="MessageLimitsTests.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham. All rights reserved.
//
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace Soulseek.Tests.Unit.Messaging
{
    using System;
    using Soulseek.Messaging;
    using Xunit;

    public class MessageLimitsTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(3, 4)]
        [InlineData(17, 4)]
        public void Rejects_Invalid_Frame_Length(int length, int minimumLength)
        {
            Assert.Throws<MessageReadException>(() => MessageLimits.ValidateFrameLength(length, minimumLength, 16));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 4)]
        [InlineData(16, 4)]
        public void Accepts_Valid_Frame_Length(int length, int minimumLength)
        {
            var exception = Record.Exception(() => MessageLimits.ValidateFrameLength(length, minimumLength, 16));
            Assert.Null(exception);
        }
    }
}
