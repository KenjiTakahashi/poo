using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using zad2;

namespace zad2tests {
    [TestFixture]
    public class CaesarStreamTest {
        [Test]
        public void Should_properly_apply_caesar_cipher() {
            MemoryStream stream = new MemoryStream();
            CaesarStream cstream = new CaesarStream(stream, 1);
            cstream.Write(Encoding.UTF8.GetBytes("teststring"), 0, 10);

            Assert.AreEqual("uftutusjoh", Encoding.UTF8.GetString(stream.ToArray()));
        }

        [Test]
        public void Should_properly_unapply_caesar_cipher() {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("uftutusjoh"));
            CaesarStream cstream = new CaesarStream(stream, 1);
            byte[] buffer = new byte[10];
            cstream.Read(buffer, 0, 10);

            Assert.AreEqual("teststring", Encoding.UTF8.GetString(buffer));
        }

        [Test]
        public void Should_properly_use_code_parameter() {
            MemoryStream stream = new MemoryStream();
            CaesarStream cstream = new CaesarStream(stream, 2);
            cstream.Write(Encoding.UTF8.GetBytes("teststring"), 0, 10);

            Assert.AreEqual("vguvuvtkpi", Encoding.UTF8.GetString(stream.ToArray()));

            cstream.Position = 0;
            byte[] buffer = new byte[10];
            cstream.Read(buffer, 0, 10);

            Assert.AreEqual("teststring", Encoding.UTF8.GetString(buffer));
        }

        [Test]
        public void Should_properly_deal_with_write_offset() {
            MemoryStream stream = new MemoryStream();
            CaesarStream cstream = new CaesarStream(stream, 1);
            cstream.Write(Encoding.UTF8.GetBytes("teststring"), 1, 9);

            Assert.AreEqual("ftutusjoh", Encoding.UTF8.GetString(stream.ToArray()));
        }

        [Test]
        public void Should_properly_deal_with_read_offset() {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("uftutusjoh"));
            CaesarStream cstream = new CaesarStream(stream, 1);
            byte[] buffer = new byte[10];
            cstream.Read(buffer, 1, 9);

            Assert.AreEqual("\0teststrin", Encoding.UTF8.GetString(buffer));
        }

        [Test]
        public void Should_properly_deal_with_write_count_limit() {
            MemoryStream stream = new MemoryStream();
            CaesarStream cstream = new CaesarStream(stream, 1);
            cstream.Write(Encoding.UTF8.GetBytes("teststring"), 0, 5);

            Assert.AreEqual("uftut", Encoding.UTF8.GetString(stream.ToArray()));
        }

        [Test]
        public void Should_properly_deal_with_read_count_limit() {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("uftutusjoh"));
            CaesarStream cstream = new CaesarStream(stream, 1);
            byte[] buffer = new byte[5];
            cstream.Read(buffer, 0, 5);

            Assert.AreEqual("tests", Encoding.UTF8.GetString(buffer));
        }
    }
}
