﻿using System;
using System.IO;

namespace Aolyn.Utility
{
	public class VarintBitConverter
	{
		/// <summary>
		/// Returns the specified byte value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">Byte value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(byte value)
		{
			return GetVarintBytes((ulong)value);
		}

		/// <summary>
		/// Returns the specified 16-bit signed value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">16-bit signed value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(short value)
		{
			var zigzag = EncodeZigZag(value, 16);
			return GetVarintBytes((ulong)zigzag);
		}

		/// <summary>
		/// Returns the specified 16-bit unsigned value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">16-bit unsigned value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(ushort value)
		{
			return GetVarintBytes((ulong)value);
		}

		/// <summary>
		/// Returns the specified 32-bit signed value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">32-bit signed value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(int value)
		{
			var zigzag = EncodeZigZag(value, 32);
			return GetVarintBytes((ulong)zigzag);
		}

		/// <summary>
		/// Returns the specified 32-bit unsigned value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">32-bit unsigned value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(uint value)
		{
			return GetVarintBytes((ulong)value);
		}

		/// <summary>
		/// Returns the specified 64-bit signed value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">64-bit signed value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(long value)
		{
			var zigzag = EncodeZigZag(value, 64);
			return GetVarintBytes((ulong)zigzag);
		}

		/// <summary>
		/// Returns the specified 64-bit unsigned value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="value">64-bit unsigned value</param>
		/// <returns>Varint array of bytes.</returns>
		public static byte[] GetVarintBytes(ulong value)
		{
			var buffer = new byte[10];
			var pos = 0;
			do
			{
				var byteVal = value & 0x7f;
				value >>= 7;

				if (value != 0)
				{
					byteVal |= 0x80;
				}

				buffer[pos++] = (byte)byteVal;

			} while (value != 0);

			var result = new byte[pos];
			Buffer.BlockCopy(buffer, 0, result, 0, pos);

			return result;
		}

		public static void WritetVarint(BinaryWriter writer, long value)
		{
			var zigzag = EncodeZigZag(value, 64);
			WritetVarintBytes(writer, (ulong)zigzag);
		}

		public static void WritetVarint(BinaryWriter writer, ulong value)
		{
			WritetVarintBytes(writer, value);
		}

		/// <summary>
		/// Returns the specified 64-bit unsigned value as varint encoded array of bytes.   
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="value">64-bit unsigned value</param>
		/// <returns>Varint array of bytes.</returns>
		private static void WritetVarintBytes(BinaryWriter writer, ulong value)
		{
			//var buffer = new byte[10];
			//var pos = 0;
			do
			{
				var byteVal = value & 0x7f;
				value >>= 7;

				if (value != 0)
				{
					byteVal |= 0x80;
				}

				//buffer[pos++] = (byte)byteVal;
				writer.Write((byte)byteVal);

			} while (value != 0);

			//var result = new byte[pos];
			//Buffer.BlockCopy(buffer, 0, result, 0, pos);
			//return result;
		}

		/// <summary>
		/// Returns byte value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>Byte value</returns>
		public static byte ToByte(byte[] bytes)
		{
			return (byte)ToTarget(bytes, 8);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>16-bit signed value</returns>
		public static short ToInt16(byte[] bytes)
		{
			var zigzag = ToTarget(bytes, 16);
			return (short)DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 16-bit usigned value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>16-bit usigned value</returns>
		public static ushort ToUInt16(byte[] bytes)
		{
			return (ushort)ToTarget(bytes, 16);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>32-bit signed value</returns>
		public static int ToInt32(byte[] bytes)
		{
			var zigzag = ToTarget(bytes, 32);
			return (int)DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 32-bit unsigned value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>32-bit unsigned value</returns>
		public static uint ToUInt32(byte[] bytes)
		{
			return (uint)ToTarget(bytes, 32);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>64-bit signed value</returns>
		public static long ToInt64(byte[] bytes)
		{
			var zigzag = ToTarget(bytes, 64);
			return DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 64-bit unsigned value from varint encoded array of bytes.
		/// </summary>
		/// <param name="bytes">Varint encoded array of bytes.</param>
		/// <returns>64-bit unsigned value</returns>
		public static ulong ToUInt64(byte[] bytes)
		{
			return ToTarget(bytes, 64);
		}

		private static long EncodeZigZag(long value, int bitLength)
		{
			return (value << 1) ^ (value >> (bitLength - 1));
		}

		private static long DecodeZigZag(ulong value)
		{
			if ((value & 0x1) == 0x1)
			{
				return (-1 * ((long)(value >> 1) + 1));
			}

			return (long)(value >> 1);
		}

		private static ulong ToTarget(byte[] bytes, int sizeBites)
		{
			int shift = 0;
			ulong result = 0;

			foreach (ulong byteValue in bytes)
			{
				ulong tmp = byteValue & 0x7f;
				result |= tmp << shift;

				if (shift > sizeBites)
				{
					throw new ArgumentOutOfRangeException("bytes", "Byte array is too large.");
				}

				if ((byteValue & 0x80) != 0x80)
				{
					return result;
				}

				shift += 7;
			}

			throw new ArgumentException("Cannot decode varint from byte array.", "bytes");
		}


		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>16-bit signed value</returns>
		public static void WriteInt16(BinaryWriter writer, Int16 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>16-bit signed value</returns>
		public static void WriteUInt16(BinaryWriter writer, UInt16 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>32-bit signed value</returns>
		public static void WriteInt32(BinaryWriter writer, Int32 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>32-bit signed value</returns>
		public static void WriteUInt32(BinaryWriter writer, UInt32 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>64-bit signed value</returns>
		public static void WriteInt64(BinaryWriter writer, Int64 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>64-bit signed value</returns>
		public static void WriteUInt64(BinaryWriter writer, UInt64 value)
		{
			var encoded = GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>16-bit signed value</returns>
		public static short ReadInt16(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 16);
			return (short)DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>16-bit signed value</returns>
		public static ushort ReadUInt16(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 16);
			return (ushort)zigzag;
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>32-bit signed value</returns>
		public static int ReadInt32(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 32);
			return (int)DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>32-bit signed value</returns>
		public static uint ReadUInt32(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 32);
			return (uint)zigzag;
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>64-bit signed value</returns>
		public static long ReadInt64(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 64);
			return DecodeZigZag(zigzag);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>64-bit signed value</returns>
		public static ulong ReadUInt64(BinaryReader reader)
		{
			var zigzag = ReadTarget(reader, 64);
			return zigzag;
		}

		private static ulong ReadTarget(BinaryReader reader, int sizeBites)
		{
			var shift = 0;
			ulong result = 0;

			while (true)
			{
				ulong byteValue = reader.ReadByte();
				ulong tmp = byteValue & 0x7f;
				result |= tmp << shift;

				if (shift > sizeBites)
				{
					throw new ArgumentOutOfRangeException(nameof(sizeBites), "Byte array is too large.");
				}

				if ((byteValue & 0x80) != 0x80)
				{
					return result;
				}

				shift += 7;
			}
		}
	}
}
