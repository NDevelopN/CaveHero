namespace Server
{
    public struct Message
    {
        public MsgType MType { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }

        public Message(MsgType mType, string text, List<string> options)
        {
            MType = mType;
            Text = text;
            Options = options;
        }
    }
}