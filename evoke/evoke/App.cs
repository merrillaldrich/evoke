using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace evoke {
    public class App : Application {
        public App() {

            // Text area for input, clear and speak buttons

            StackLayout textarea = new StackLayout { Orientation = StackOrientation.Horizontal };
            Entry textentry = new Entry { HorizontalOptions= LayoutOptions.FillAndExpand, FontSize = 24 };
            textentry.Completed += (sender, e) => {
                this.speak(textentry.Text);
            };

            Button clearbutton = new Button { Text = "X", WidthRequest = 60, HeightRequest = 60 };
            clearbutton.Clicked += (sender, e) => {
                textentry.Text = "";
            };

            Button speakbutton = new Button { Text = "S", WidthRequest = 60, HeightRequest = 60 };
            speakbutton.Clicked += (sender, e) => {
                if (textentry.Text.Trim().Length > 0) { this.speak(textentry.Text); };
            };

            textarea.Children.Add(textentry);
            textarea.Children.Add(clearbutton);
            textarea.Children.Add(speakbutton);
            vocablibrary l = new vocablibrary();

            var categories = new Dictionary<string, List<word>>();

            foreach (word w in l.Lib) {
                if (!categories.ContainsKey(w.category)) {
                    categories.Add(w.category, new List<word>());
                }
                categories[w.category].Add(w);
            }

            // Scrolling library of pictographs

            StackLayout libstack = new StackLayout { };

            foreach (string catname in categories.Keys) {
                Label cl = new Label { Text = catname, FontSize = 24 };
                libstack.Children.Add(cl);

                // Set up a grid that can contain however many words in the current category
                int numcols = 6;
                Grid g = new Grid();

                for (int i = 0; i < Math.Ceiling(categories[catname].Count / (decimal)numcols); i++) {
                    g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) });
                }
                for (int i = 0; i < numcols; i++) {
                    g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });
                }

                // Fill in the grid walking across each row, then down to the next row
                int r = 0;
                int c = 0;
                foreach (word w in categories[catname]) {

                    Image i = l.getimage(w.image);

                    var tgr = new TapGestureRecognizer();
                    tgr.NumberOfTapsRequired = 1;
                    tgr.Tapped += (sender, e) => {
                        textentry.Text = (textentry.Text + " " + w.value);
                    };
                    i.GestureRecognizers.Add(tgr);

                    g.Children.Add(i, c, r);

                    if (c < g.ColumnDefinitions.Count - 1) {
                        c++;
                    }
                    else {
                        c = 0;
                        r++;
                    }
                }
                libstack.Children.Add(g);
            }

            ScrollView libraryscroll = new ScrollView { Content = libstack, VerticalOptions = LayoutOptions.FillAndExpand };

            MainPage = new ContentPage {
                Content = new StackLayout {
                    Padding = 10,
                    Children = {
                        textarea, libraryscroll
                    }
                }
            };
        }
        protected void speak(string phrase) {
            DependencyService.Get<ITextToSpeech>().Speak(phrase);
        }
        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
