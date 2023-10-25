using System.Collections.Concurrent;

namespace Server
{
    public class IOBuffer
    {
        private static BlockingCollection<Message> _input = new();
        private static BlockingCollection<Message> _output = new();

        public static void WriteInput(Message input)
        {
            _input.Add(input);
        }

        public static Message NextInput()
        {
            while (_input.Count == 0) { }
            return _input.Take();
        }

        ////////////////////////////////////////////////

        public static void WriteHeading(string header)
        {
            Message msg = new Message(MsgType.Heading, header, null);
            _output.Add(msg);
        }

        public static void WriteMsg()
        {
            WriteMsg("");
        }

        public static void WriteMsg(string text)
        {
            Message msg = new Message(MsgType.Message, text, new List<string>());
            _output.Add(msg);
        }

        public static void WriteOption(string prompt, List<string> options)
        {
            Message msg = new Message(MsgType.Option, prompt, options);
            _output.Add(msg);
        }

        public static Message NextOutput()
        {
            while (_output.Count == 0) { }
            return _output.Take();
        }
    }
}