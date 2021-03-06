﻿using System.Collections.Generic;
using NUnit.Framework;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.Tests.UITests
{
    [TestFixture]
    public class LatexHelpersTests
    {
        private LatexHelpers latexHelpers
            = new LatexHelpers();

        private LatexDocument document
            = new LatexDocument();

        [Test]
        public void GetLatexTable()
        {
            List<TestObject> data = new List<TestObject>();

            data.Add(new TestObject { ID = 1, Name = "A" });
            data.Add(new TestObject { ID = 2, Name = "B" });
            data.Add(new TestObject { ID = 3, Name = "C" });

            var idColumn = new LatexColumn<TestObject>
                ("ID", (TestObject to) => (2 * to.ID).ToString());

            var nameColumn = new LatexColumn<TestObject>
                ("Name", (TestObject to) => to.Name);

            /*string result =document.GetLatexTable<TestObject>
                (data, idColumn, nameColumn);

            Trace.Write(result);*/
        }

        [Test]
        public void LatexEncodeText()
        {
            string result
                = LatexDocument.EncodeText
                ("Æg rød på træ Åben Ør");
            Assert.AreEqual
                (@"\AE g r\o d p\aa\ tr\ae\ \AA ben \O r"
                , result);
        }
    
        private class TestObject
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }
    }
}