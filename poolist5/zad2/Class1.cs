using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    public class CaesarStream : Stream {
        private Stream _stream;
        private int _code;

        public CaesarStream(Stream stream, int code) : base() {
            this._stream = stream;
            this._code = code;
        }

        public override int Read(byte[] buffer, int offset, int count) {
            byte[] tempbuffer = new byte[count + offset];
            int retvalue = this._stream.Read(tempbuffer, offset, count);
            for(int i = offset; i < offset + retvalue; ++i)
                buffer[i] = (byte)((char)tempbuffer[i] - this._code);
            return retvalue;
        }

        public override void Write(byte[] buffer, int offset, int count) {
            byte[] tempbuffer = new byte[count + offset];
            for(int i = offset; i < offset + count; ++i) {
                tempbuffer[i] = (byte)((char)buffer[i] + this._code);
            }
            this._stream.Write(tempbuffer, offset, count);
        }

        public override bool CanRead {
            get { return this._stream.CanRead; }
        }

        public override bool CanSeek {
            get { return this._stream.CanSeek; }
        }

        public override bool CanWrite {
            get { return this._stream.CanWrite; }
        }

        public override void Flush() {
            this._stream.Flush();
        }

        public override long Length {
            get { return this._stream.Length; }
        }

        public override long Position {
            get { return this._stream.Position; }
            set { this._stream.Position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return this._stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            this._stream.SetLength(value);
        }

        public byte[] ToArray() {
            byte[] tempbuffer = new byte[this.Length];
            this.Read(tempbuffer, 0, (int)this.Length);
            return tempbuffer;
        }
    }
}
