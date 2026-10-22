// <copyright file="LimitedMemoryStream.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham.
//
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, version 3.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see https://www.gnu.org/licenses/.
//
//     This program is distributed with Additional Terms pursuant to Section 7
//     of the GPLv3.  See the LICENSE file in the root directory of this
//     project for the complete terms and conditions.
//
//     SPDX-FileCopyrightText: JP Dillingham
//     SPDX-License-Identifier: GPL-3.0-only
// </copyright>

namespace Soulseek.Messaging
{
    using System;
    using System.IO;

    /// <summary>
    ///     Rejects decompressed output before it can exceed the configured limit.
    /// </summary>
    internal sealed class LimitedMemoryStream : MemoryStream
    {
        private readonly int maxLength;

        internal LimitedMemoryStream(int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            this.maxLength = maxLength;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (count > maxLength - Position)
            {
                throw new IOException($"Decompressed payload exceeds {maxLength} bytes");
            }

            base.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            if (Position >= maxLength)
            {
                throw new IOException($"Decompressed payload exceeds {maxLength} bytes");
            }

            base.WriteByte(value);
        }
    }
}
