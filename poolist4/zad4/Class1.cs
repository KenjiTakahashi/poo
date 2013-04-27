using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    public class TagBuilder {
        public bool IsIndented { get; set; }
        public int Indentation { get; set; }
        private bool _prettyPrint;
        public bool prettyPrint {
            get {
                return _prettyPrint;
            }
            set {
                _prettyPrint = value;
                IsIndented = value;
            }
        }
        private int _level;

        public TagBuilder() {
            this.IsIndented = false;
            this.Indentation = 4;
            this.prettyPrint = false;
            this._level = 0;
        }

        public TagBuilder(string TagName, TagBuilder Parent) : this() {
            this.tagName = TagName;
            this.parent = Parent;
            this._level = Parent._level + 1;
            this.prettyPrint = this.parent.prettyPrint;
            this.Indentation = this.parent.Indentation;
            this.IsIndented = this.parent.IsIndented;
        }

        private string tagName;
        private TagBuilder parent;
        private StringBuilder body = new StringBuilder();
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();

        public TagBuilder AddContent(string Content) {
            this.Indent(body);
            body.Append(Content);
            this.Prettify(body);
            return this;
        }

        public TagBuilder AddContentFormat(string Format, params object[] args) {
            body.AppendFormat(Format, args);
            return this;
        }

        public TagBuilder StartTag(string TagName) {
            TagBuilder tag = new TagBuilder(TagName, this);
            return tag;
        }

        public TagBuilder EndTag() {
            parent.AddContent(this.ToString());
            return parent;
        }

        public TagBuilder AddAttribute(string Name, string Value) {
            _attributes.Add(Name, Value);
            return this;
        }

        public override string ToString() {
            StringBuilder tag = new StringBuilder();
            // preamble
            if(!string.IsNullOrEmpty(this.tagName)) {
                tag.AppendFormat("<{0}", tagName);
            }
            if(_attributes.Count > 0) {
                tag.Append(" ");
                tag.Append(
                    string.Join(" ",
                        _attributes.Select(
                            kvp => string.Format("{0}='{1}'", kvp.Key, kvp.Value)
                        ).ToArray()
                    )
                );
            }
            // body/ending
            if(body.Length > 0) {
                if(!string.IsNullOrEmpty(this.tagName) || this._attributes.Count > 0) {
                    tag.Append(">");
                    this.Prettify(tag);
                }
                tag.Append(body.ToString());
                if(!string.IsNullOrEmpty(this.tagName)) {
                    this.Indent(tag, -1);
                    tag.AppendFormat("</{0}>", this.tagName);
                }
            } else {
                if(!string.IsNullOrEmpty(this.tagName)) {
                    tag.Append("/>");
                }
            }
            return tag.ToString();
        }

        private void Prettify(StringBuilder tag) {
            if(this.prettyPrint) {
                tag.Append("\n");
            }
        }

        private void Indent(StringBuilder tag, int modifier = 0) {
            if(this.IsIndented) {
                tag.Append(new string(' ', (this._level + modifier) * this.Indentation));
            }
        }
    }
}