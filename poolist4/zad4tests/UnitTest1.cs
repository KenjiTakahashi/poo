using NUnit.Framework;
using System;
using zad4;

namespace zad4tests {
    [TestFixture]
    public class TagBuilderTest {
        [Test]
        public void Returns_properly_indented_child() {
            TagBuilder tag = new TagBuilder();
            tag.IsIndented = true;
            var result = tag.StartTag("parent").AddContent("normal_child").EndTag().ToString();

            Assert.AreEqual("<parent>    normal_child</parent>", result);
        }

        [Test]
        public void Returns_properly_indented_child_tag() {
            TagBuilder tag = new TagBuilder();
            tag.IsIndented = true;
            var result = tag.StartTag("parent").StartTag("child").EndTag().EndTag().ToString();

            Assert.AreEqual("<parent>    <child/></parent>", result);
        }

        [Test]
        public void Returns_properly_indented_child_tag_with_contents() {
            TagBuilder tag = new TagBuilder();
            tag.IsIndented = true;
            var result = tag.StartTag("parent").StartTag("child").AddContent("content").EndTag().EndTag().ToString();

            Assert.AreEqual("<parent>    <child>        content    </child></parent>", result);
        }

        [Test]
        public void Returns_properties_within_same_line() {
            // no indent
            TagBuilder tag = new TagBuilder();
            tag.IsIndented = true;
            var result = tag.StartTag("parent").AddAttribute("attr", "val").AddContent("content").EndTag().ToString();

            Assert.AreEqual("<parent attr='val'>    content</parent>", result);
        }

        [Test]
        public void Properly_handles_indentation_depth() {
            TagBuilder tag = new TagBuilder();
            tag.IsIndented = true;
            tag.Indentation = 2; // hipsters'
            var result = tag.StartTag("parent").AddContent("content").EndTag().ToString();

            Assert.AreEqual("<parent>  content</parent>", result);
        }

        [Test]
        public void Prettyprint_test() {
            TagBuilder tag = new TagBuilder();
            tag.prettyPrint = true;
            var result = tag.StartTag("parent").StartTag("child").AddContent("content").EndTag().EndTag().ToString();

            Assert.AreEqual("<parent>\n    <child>\n        content\n    </child>\n</parent>\n", result);
        }
    }
}
