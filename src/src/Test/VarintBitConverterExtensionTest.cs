using System;
using System.IO;
using System.Linq;
using Aolyn.Utility;
using Xunit;

namespace Test
{
	public class VarintBitConverterExtensionTest
	{
		[Fact]
		public void VarintEncodeAndDecodeByteMaxValue()
		{
			const byte number = Byte.MaxValue;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ToByte(encoded));
		}

		[Fact]
		public void VarintEncodeAndDecodeByteMinValue()
		{
			const byte number = Byte.MinValue;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ToByte(encoded));
		}

		[Fact]
		public void VarintEncodeAndDecodeByteZero()
		{
			const byte number = 0;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ToByte(encoded));
		}

		[Fact]
		public void VarintEncodeAndDecodeByte()
		{
			const byte number = Byte.MaxValue / 2;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ToByte(encoded));
		}


		[Fact]
		public void VarintEncodeAndDecodeInt16MaxValue()
		{
			const short number = Int16.MaxValue;
			TestInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt16MinValue()
		{
			const short number = Int16.MinValue;
			TestInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt16Zero()
		{
			const short number = 0;
			TestInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt16()
		{
			const short number = Int16.MaxValue / 2;
			TestInt16(number);
		}


		[Fact]
		public void VarintEncodeAndDecodeUInt16MaxValue()
		{
			const ushort number = UInt16.MaxValue;
			TestUInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt16MinValue()
		{
			const ushort number = UInt16.MinValue;
			TestUInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeZero()
		{
			const ushort number = UInt16.MaxValue / 2;
			TestUInt16(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt16()
		{
			const ushort number = UInt16.MaxValue / 2;
			TestUInt16(number);
		}


		[Fact]
		public void VarintEncodeAndDecodeInt32MaxValue()
		{
			const int number = Int32.MaxValue;
			TestInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt32MinValue()
		{
			const int number = Int32.MinValue;
			TestInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt32Zero()
		{
			const int number = 0;
			TestInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt32()
		{
			const int number = Int32.MaxValue / 2;
			TestInt32(number);
		}


		[Fact]
		public void VarintEncodeAndDecodeUInt32MaxValue()
		{
			const uint number = UInt32.MaxValue;
			TestUInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt32MinValue()
		{
			const uint number = UInt32.MinValue;
			TestUInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt32Zero()
		{
			const uint number = 0;
			TestUInt32(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt32()
		{
			const uint number = UInt32.MaxValue / 2;
			TestUInt32(number);
		}


		[Fact]
		public void VarintEncodeAndDecodeInt64MaxValue()
		{
			const long number = Int64.MaxValue;
			TestInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt64MinValue()
		{
			const long number = Int64.MinValue;
			TestInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt64Zero()
		{
			const long number = 0;
			TestInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeInt64()
		{
			const long number = Int64.MaxValue / 2;
			TestInt64(number);
		}


		[Fact]
		public void VarintEncodeAndDecodeUInt64MaxValue()
		{
			const ulong number = UInt64.MaxValue;
			TestUInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt64MinValue()
		{
			const ulong number = UInt64.MinValue;
			TestUInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt64Zero()
		{
			const ulong number = 0;
			TestUInt64(number);
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt64()
		{
			const ulong number = UInt64.MaxValue / 2;
			TestUInt64(number);
		}


		private static void TestUInt16(ushort number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarUInt16());
		}

		private static void TestInt16(short number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarInt16());
		}

		private static void TestUInt32(uint number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarUInt32());
		}

		private static void TestInt32(int number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarInt32());
		}

		private static void TestUInt64(ulong number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarUInt64());
		}

		private static void TestInt64(long number)
		{
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			writer.WriteVarint(number);
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			Assert.Equal(number, reader.ReadVarInt64());
		}


		[Fact]
		public void VarintEncodeAndDecodeUInt64Read()
		{
			const ulong number = UInt64.MaxValue / 2;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ReadUInt64(new BinaryReader(new MemoryStream(encoded))));
		}

		[Fact]
		public void VarintEncodeAndDecodeUInt64MoreBytes()
		{
			const ulong number = UInt64.MaxValue / 2;
			var encoded = VarintBitConverter.GetVarintBytes(number);
			Assert.Equal(number, VarintBitConverter.ToUInt64(encoded));

			var encoded2 = encoded.Concat(new byte[] { 0xa, 0xb }).ToArray();
			Assert.Equal(number, VarintBitConverter.ToUInt64(encoded2));
		}
	}
}
