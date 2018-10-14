using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Android.Widget;
using System;
using System.IO;
using MyToDoList;
using Android.Content;
using Android.App;

namespace MyToDoListUnitTest
{

    [TestClass]
    public class UnitTest1
    {
        
        private int count = 0;   
        private readonly string item;
        public List<string> items { get; set; }
        public EditText itemText;
        
        [TestMethod]
        public void LoadList()
        {
            if (count > 0)
            {
                if (item != null)
                {
                    items.Add(item); // add item in the list
                }
                Assert.IsTrue(item != null);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            string item = itemText.Text;

            if (item == "" || item == null)   //
            {
                return;
            }

            items.Add(item);
            Assert.IsTrue(item != null);
        }
    }
}
