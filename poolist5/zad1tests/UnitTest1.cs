using netDumbster.smtp;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using zad1;

namespace zad1tests {
    [TestFixture]
    public class SmtpFacadeTest {
        SimpleSmtpServer _server;
        [TestFixtureSetUp]
        public void FixtureSetUp() {
            this._server = SimpleSmtpServer.Start(25000);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown() {
            this._server.Stop();
        }

        [SetUp]
        public void SetUp() {
            this._server.ClearReceivedEmail();
        }

        [Test]
        public void Should_send_an_email() {
            SmtpFacade facade = new SmtpFacade("localhost", 25000);
            facade.Send("me@mail.com", "you@mail.com", "Empty", "N/A");

            Assert.AreEqual(1, this._server.ReceivedEmailCount);
            Assert.AreEqual("me@mail.com", this._server.ReceivedEmail[0].Headers["from"]);
            Assert.AreEqual("you@mail.com", this._server.ReceivedEmail[0].Headers["to"]);
            Assert.AreEqual("Empty", this._server.ReceivedEmail[0].Headers["subject"]);
            Assert.AreEqual("N/A", this._server.ReceivedEmail[0].MessageParts[0].BodyData);
        }

        [Test]
        public void Should_send_an_attachment() {
            SmtpFacade facade = new SmtpFacade("localhost", 25000);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("function() { return 1; }"));
            facade.Send("me@mail.com", "you@mail.com", "Empty", "N/A", stream);

            Assert.AreEqual(1, this._server.ReceivedEmailCount);

            SmtpMessagePart[] parts = this._server.ReceivedEmail[0].MessageParts;

            Assert.AreEqual(2, parts.Length);

            string attr = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1].BodyData));

            Assert.AreEqual("function() { return 1; }", attr);
        }

        [Test]
        public void Should_send_an_attachment_with_mime_type() {
            SmtpFacade facade = new SmtpFacade("localhost", 25000);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("function() { return 1; }"));
            facade.Send("me@mail.com", "you@mail.com", "Empty", "N/A", stream, "application/javascript");

            Assert.AreEqual(1, this._server.ReceivedEmailCount);

            SmtpMessagePart[] parts = this._server.ReceivedEmail[0].MessageParts;

            Assert.AreEqual(2, parts.Length);
            Assert.AreEqual("application/javascript", parts[1].Headers["content-type"]);
        }
    }
}
