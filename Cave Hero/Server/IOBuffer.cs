using System.Collections.Concurrent;

namespace CaveHero.Server
{
    public class IOBuffer
    {
        private BlockingCollection<Message> _input = new();
        private BlockingCollection<Message> _output = new();

        public void WriteInput(Message input)
        {
            _input.Add(input);
        }

        public Message NextInput()
        {
            return _input.Take();
        }

        ////////////////////////////////////////////////

        public void WriteHeading(string header)
        {
            Message msg = new Message(MsgType.Heading, header, new());
            _output.Add(msg);
        }

        public void WriteMsg()
        {
            WriteMsg("");
        }

        public void WriteMsg(string text)
        {
            Message msg = new Message(MsgType.Message, text, new());
            _output.Add(msg);
        }

        public void WriteOption(string prompt, List<string> options)
        {
            Message msg = new Message(MsgType.Option, prompt, options);
            _output.Add(msg);
        }

        public Message NextOutput()
        {
            return _output.Take();
        }
    }
}