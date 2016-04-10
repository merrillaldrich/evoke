using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using PCLStorage;

namespace evoke
{
    public struct word { public string value; public string category; public string image; }


    class vocablibrary
    {
        private List<word> _lib;
        public vocablibrary()
        {
            _lib = loadVocab();
        }

        public List<word> Lib
        {
            get
            {
                return _lib;
            }

            set
            {
                _lib = value;
            }
        }
        private List<word> loadVocab() {

            var l = new List<word>();
            IFolder folder = FileSystem.Current.LocalStorage;

            IFile libfile = folder.GetFileAsync("vocablist.txt").Result;

            string[] content = libfile.ReadAllTextAsync().Result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in content) {
                var tokens = line.Split('\t');
                word w = new word { value = tokens[0], category = tokens[1], image = tokens[2] };
                l.Add(w);
            }
            return l;
        }

        public Image getimage ( string filename ) {
            Image i = new Image();
            var f = FileSystem.Current.LocalStorage.GetFileAsync(filename).Result;
            Stream s =  f.OpenAsync(PCLStorage.FileAccess.Read).Result;
            i.Source = ImageSource.FromStream(() => s);
            return i;
        }
    }
}
