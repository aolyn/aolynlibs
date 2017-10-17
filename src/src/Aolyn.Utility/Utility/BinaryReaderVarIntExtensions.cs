using Aolyn.Utility;

// ReSharper disable once CheckNamespace
namespace System.IO
{
	public static class BinaryReaderVarIntExtensions
	{
		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>16-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, short value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>16-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, ushort value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>32-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, int value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>32-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, uint value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>64-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, long value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="writer">Varint encoded array of bytes.</param>
		/// <param name="value"></param>
		/// <returns>64-bit signed value</returns>
		public static void WriteVarint(this BinaryWriter writer, ulong value)
		{
			var encoded = VarintBitConverter.GetVarintBytes(value);
			writer.Write(encoded);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>16-bit signed value</returns>
		public static short ReadVarInt16(this BinaryReader reader)
		{
			return VarintBitConverter.ReadInt16(reader);
		}

		/// <summary>
		/// Returns 16-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>16-bit signed value</returns>
		public static ushort ReadVarUInt16(this BinaryReader reader)
		{
			return VarintBitConverter.ReadUInt16(reader);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>32-bit signed value</returns>
		public static int ReadVarInt32(this BinaryReader reader)
		{
			return VarintBitConverter.ReadInt32(reader);
		}

		/// <summary>
		/// Returns 32-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>32-bit signed value</returns>
		public static uint ReadVarUInt32(this BinaryReader reader)
		{
			return VarintBitConverter.ReadUInt32(reader);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>64-bit signed value</returns>
		public static long ReadVarInt64(this BinaryReader reader)
		{
			return VarintBitConverter.ReadInt64(reader);
		}

		/// <summary>
		/// Returns 64-bit signed value from varint encoded array of bytes.
		/// </summary>
		/// <param name="reader">Varint encoded array of bytes.</param>
		/// <returns>64-bit signed value</returns>
		public static ulong ReadVarUInt64(this BinaryReader reader)
		{
			return VarintBitConverter.ReadUInt64(reader);
		}
	}
}