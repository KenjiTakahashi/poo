using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public abstract class Department {
        protected Department _next;

        public Department SetNext(Department next) {
            this._next = next;
            return this._next;
        }

        public abstract void ProcessLetter(string request);
    }

    public class President : Department {
        public override void ProcessLetter(string request) {
            if(request.ToLower().Contains("praise"))
                Console.WriteLine("President received the letter: " + request);
            else if(this._next != null)
                this._next.ProcessLetter(request);
        }
    }

    public class LawSection : Department {
        public override void ProcessLetter(string request) {
            if(request.ToLower().Contains("complaint"))
                Console.WriteLine("Law section received the letter: " + request);
            else if(this._next != null)
                this._next.ProcessLetter(request);
        }
    }

    public class TradingSection : Department {
        public override void ProcessLetter(string request) {
            if(request.ToLower().Contains("order"))
                Console.WriteLine("Trading section received the letter: " + request);
            else if(this._next != null)
                this._next.ProcessLetter(request);
        }
    }

    public class MarketingSection : Department {
        public override void ProcessLetter(string request) {
            Console.WriteLine("Marketing section received the letter: " + request);
            if(this._next != null)
                this._next.ProcessLetter(request);
        }
    }

    public class Archiver : Department {
        private Stream _logger;

        public Archiver(Stream logger) {
            this._logger = logger;
        }

        public override void ProcessLetter(string request) {
            _logger.Write(Encoding.UTF8.GetBytes(request), 0, request.Length);
        }
    }

    public class LetterDispatcher : Department {
        public LetterDispatcher(Stream logger = null) {
            (logger != null ? this.SetNext(new Archiver(logger)) : this.SetNext(new President()))
                .SetNext(new LawSection())
                .SetNext(new TradingSection())
                .SetNext(new MarketingSection());
        }

        public override void ProcessLetter(string request) {
            this._next.ProcessLetter(request);
        }
    }
}
