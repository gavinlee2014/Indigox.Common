using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Indigox.Common.Logging.Test
{
    [TestFixture]
    public class LogTest
    {
        private MemoryStream stream;
        private StreamWriter outputStreamWriter;
        private StreamReader outputStreamReader;
        private string log;
        private TextWriter orginalOutput;

        int id;
        string title;
        string message;
        string category;
        string[] categories;
        Dictionary<string, object> context;
        Exception exception;

        public LogTest()
        {
            stream = new MemoryStream();
            outputStreamWriter = new StreamWriter(stream);
            outputStreamReader = new StreamReader(stream);
            orginalOutput = Console.Out;

            id = 0;
            title = "title";
            message = "message";
            category = "blue";
            categories = new string[] { category, "indigo", "violet" };
            context = new Dictionary<string, object>();
            context.Add("id", id);
            context.Add("title", title);
            context.Add("categories", categories);
            exception = new NullReferenceException();
        }

        [SetUp]
        public void SetOutput()
        {
            Console.SetOut(outputStreamWriter);
        }
        [TearDown]
        public void TearDown()
        {
            stream.SetLength(0);
            Console.SetOut(orginalOutput);
            Console.Write(log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo01()
        {
            Log.Info(title);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:, categories:, context:, exception: \r\n", title), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo02()
        {
            Log.Info(title, message);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:{1}, categories:, context:, exception: \r\n", title, message), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo03()
        {
            Log.Info(title, context);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:, categories:, context:{1}, exception: \r\n", title, Context2String(context)), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo04()
        {
            Log.Info(title, message, context);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:{1}, categories:, context:{2}, exception: \r\n", title, message, Context2String(context)), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo05()
        {
            Log.Info(title, context, exception);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:, categories:, context:{1}, exception:{2} \r\n", title, Context2String(context), exception), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo06()
        {
            Log.Info(title, message, context, exception);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:0, title:{0}, message:{1}, categories:, context:{2}, exception:{3} \r\n", title, message, Context2String(context), exception), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo07()
        {
            Log.Info(id, title, message, category);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:{0}, title:{1}, message:{2}, categories:{3}, context:, exception: \r\n", id, title, message, category), log);

        }
        //TODO: byYi
        //[Test]
        public void TestInfo08()
        {
            Log.Info(id, title, message, categories);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:{0}, title:{1}, message:{2}, categories:{3}, context:, exception: \r\n", id, title, message, string.Join(";", categories)), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo09()
        {
            Log.Info(id, title, message, category, context);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:{0}, title:{1}, message:{2}, categories:{3}, context:{4}, exception: \r\n", id, title, message, category, Context2String(context)), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo10()
        {
            Log.Info(id, title, message, categories, context);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:{0}, title:{1}, message:{2}, categories:{3}, context:{4}, exception: \r\n", id, title, message, string.Join(";", categories), Context2String(context)), log);
        }
        //TODO: byYi
        //[Test]
        public void TestInfo11()
        {
            Log.Info(id, title, message, categories, context, exception);
            outputStreamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            log = outputStreamReader.ReadToEnd();
            Assert.AreEqual(string.Format("id:{0}, title:{1}, message:{2}, categories:{3}, context:{4}, exception:{5} \r\n", id, title, message, string.Join(";", categories), Context2String(context), exception), log);
        }

        private string Context2String(Dictionary<string,object> context)
        {
            string[] contextString = new string[context.Count];

            int i = 0;
            foreach (KeyValuePair<string, object> pair in context)
            {
                contextString[i++] = string.Format("{0}:{1}", pair.Key, pair.Value);
            }
            return "{" + string.Join(",", contextString) + "}";
        }
    }
}
