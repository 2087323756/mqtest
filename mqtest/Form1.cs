using System;
using System.Windows.Forms;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace mqtest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static IConnectionFactory _factory = null;
        static IConnection _connection = null;
        static ITextMessage _message = null;
        static ITextMessage _message2 = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (_factory == null)
                _factory = new ConnectionFactory(tbUrl.Text);
            try
            {
                using (_connection = _factory.CreateConnection())
                {
                    using (ISession session = _connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                    {
                        IDestination destination = new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(tbTopic.Text);
                        IMessageProducer producer = session.CreateProducer(destination);
                        _message = producer.CreateTextMessage(richTextBox1.Text);
                        producer.Send(_message, MsgDeliveryMode.Persistent, MsgPriority.Normal, TimeSpan.MaxValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
