using System;
using System.IO;

namespace Aolyn.IO
{
	public class ArraySegmentStream : Stream
	{
		public int SegmentIndex { get; private set; }
		public int SegmentPosition { get; private set; }
		public ArraySegment<byte>[] Datas { get; private set; }

		public ArraySegmentStream() { }

		public ArraySegmentStream(ArraySegment<byte>[] datas)
		{
			Reset(datas);
		}

		public void Reset(ArraySegment<byte>[] datas)
		{
			Datas = datas;
			SegmentIndex = 0;
			SegmentPosition = 0;
		}

		public override void Flush()
		{
			//throw new NotImplementedException();
		}

		public override int ReadByte()
		{
			if (GetLeftCount() == 0) return -1;
			var buffer = new byte[0];
			Read(buffer, 0, buffer.Length);
			return buffer[0];
		}

#if NETCORE
		//public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		//{
		//	//return base.BeginRead(buffer, offset, count, callback, state);
		//	throw new NotSupportedException();
		//}

		//public override int EndRead(IAsyncResult asyncResult)
		//{
		//	//return base.EndRead(asyncResult);
		//	throw new NotSupportedException();
		//}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return Task.FromResult(Read(buffer, offset, count));
		}
#endif

		public override int Read(byte[] buffer, int offset, int count)
		{
			var leftTotalCount = GetLeftCount();
			if (leftTotalCount < count)
				count = leftTotalCount;
			//throw new IOException("no enough buffer data");

			var bufferIndex = 0;
			for (var i = SegmentIndex; i < Datas.Length; i++)
			{
				var leftCount = count - bufferIndex;
				var segment = Datas[SegmentIndex];
				var currentSegmentLeftCount = segment.Count - SegmentPosition;
				var readCount = Math.Min(leftCount, currentSegmentLeftCount);
				Array.Copy(segment.Array, segment.Offset + SegmentPosition,
					buffer, offset + bufferIndex, readCount);

				SegmentPosition += readCount;
				bufferIndex += readCount;

				if (SegmentPosition == segment.Count)
				{
					//move to next segment
					SegmentPosition = 0;
					SegmentIndex++;
				}
				if (bufferIndex != count) continue;

				break;
			}
			return bufferIndex;
		}

		private int GetLeftCount()
		{
			return GetLeftCount(Datas, SegmentIndex, SegmentPosition);
		}

		public static int GetLeftCount(ArraySegment<byte>[] segments, int currentSegmentIndex, int currentSegmentPosition)
		{
			var count = 0;
			var isCurrent = true;
			for (var i = currentSegmentIndex; i < segments.Length; i++)
			{
				count += isCurrent
					? segments[i].Count - currentSegmentPosition
					: segments[i].Count;
				isCurrent = false;
			}
			return count;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public override bool CanRead { get; } = true;
		public override bool CanSeek { get; } = false;
		public override bool CanWrite { get; } = false;
		public override long Length => GetLeftCount(Datas, 0, 0);
		public override long Position
		{
			get => Length - GetLeftCount();
			set
			{
				if (value + 1 > Length) throw new InvalidOperationException("position out of range");

				var position = 0;
				for (int segmentIndex = 0; segmentIndex < Datas.Length; segmentIndex++)
				{
					if (position + Datas[segmentIndex].Count - 1 < value)
					{
						position += Datas[segmentIndex].Count; //next segment first element position
					}
					else
					{
						SegmentIndex = segmentIndex;
						SegmentPosition = (int)value - position;
						return;
					}
				}
				throw new IndexOutOfRangeException();
			}
		}
	}
}
