using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ShuffleIt
{
    public static class Data
    {
        public static Setting<List<Item>> items = new Setting<List<Item>>("items", new List<Item>());
    }
}
