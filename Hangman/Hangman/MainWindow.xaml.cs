using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VB_tripla
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string WordToGuess;
        private List<char> guessedLetters;
        private int wrongGuesses;
        private int notWrongGuesses;
        private List<string> fikagolyo;
        private CancellationTokenSource _cancellationTokenSource;
        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
            StartBackgroundLoop();
        }

        private void StartNewGame()
        {
            Random random = new Random();
            fikagolyo = new List<string> {"alma", "fa", "ház", "autó", "kutya", "macska", "tenger", "tó", "hó", "eső", "nap", "hold", "csillag", "hegy", "völgy", "erdő", "rét", "fű", "virág", "fáklya",
    "kép", "szem", "száj", "fül", "kéz", "láb", "fej", "tüdő", "szív", "agy", "képeslap", "bor", "sör", "víz", "kávé", "tea", "túró", "sajt", "kenyér", "zöldség",
    "gyümölcs", "hús", "tojás", "vaj", "tészta", "pizza", "leves", "főzelék", "szendvics", "csirke", "marha", "sertés", "hal", "rizs", "krumpli", "hagyma", "bors",
    "fűszer", "kecskesajt", "képregény", "film", "zene", "tánc", "színház", "képzőművészet", "irodalom", "könyv", "újság", "internet", "telefon", "számítógép", "laptop",
    "tablet", "okostelefon", "autóbusz", "vonat", "repülő", "bicikli", "motorkerékpár", "autóvezetés", "szabadság", "munka", "iskola", "egyetem", "főiskola", "tanár",
    "diák", "feladat", "vizsga", "témakör", "tudomány", "kutatás", "fejlesztés", "programozás", "matematika", "fizika", "kémia", "biológia", "geográfia", "történelem",
    "nyelv", "irodalom", "művészet", "pszichológia", "szociológia", "személyiség", "karakter", "képesség", "tehetség", "gondolat", "vélemény", "hit", "vallás", "személy",
    "család", "barát", "ellenség", "cél", "álom", "tárgy", "játék", "sport", "futás", "úszás", "labda", "képesség", "erő", "bátorság", "öröm", "boldogság", "szomorúság",
    "harag", "düh", "szorongás", "félelem", "szégyen", "kudarc", "siker", "számla", "pénz", "gazdagság", "szegénység", "adomány", "adó", "támogatás", "segítség",
    "nélkülözés", "jólét", "szegénység", "egészség", "betegség", "orvos", "kórház", "gyógyszer", "műtét", "seb", "fertőzés", "vakcina", "kezelés", "terápia", "egészséges",
    "beteg", "szikra", "tűz", "láng", "szél", "fény", "sötét", "látás", "hallás", "írás", "olvasás", "kommunikáció", "beszéd", "nyelv", "nyelvészet", "elmélet", "gyakorlat",
    "hely", "város", "falu", "település", "tér", "utca", "ház", "szoba", "ágy", "asztal", "szék", "kanapé", "polc", "szekrény", "ablak", "ajtó", "fényképezőgép", "tv",
    "kamera", "mikrofon", "hangszóró", "klaviatúra", "egér", "monitor", "géptáblázat", "program", "alkalmazás", "weblap", "oldal", "link", "domén", "szám", "adott", "valós",
    "képesség", "szokás", "tradíció", "szopás", "cselekvés", "tett", "napi", "rutin", "jövő", "történet", "valóság", "valódi", "tény", "hazugság", "titok", "igazság",
    "hamis", "lehetőség", "képzelődés", "valóság", "bizonyság", "kép", "munkája", "tanulás", "oktatás", "tapintat", "hatás", "dolog", "tett", "cselekedet", "autóbusz", "fényképezőgép", "számítógép", "laptop", "tablet", "okostelefon", "vonat", "repülő", "bicikli","almafa", "szerszám", "fúrógép", "kalapács", "csavar", "csavarhúzó", "fogó", "kéziszerszám",
    "horgászat", "halászat", "művész", "festmény", "szobor", "fotó", "kamera", "videó", "hang", "zene",
    "hangszóró", "mikrofon", "gitár", "hegedű", "zongora", "dob", "ének", "koncert", "rádió", "televízió",
    "csatorna", "film", "színész", "rendező", "színház", "opera", "balett", "táncos", "előadás", "vers",
    "novella", "könyvtár", "könyv", "lap", "magazin", "újság", "cikk", "interjú", "kritika", "próza", "költő",
    "író", "novellista", "regény", "téma", "fiók", "asztal", "íróasztal", "szekrény", "polc", "szék", "kanapé",
    "ágy", "párna", "takaró", "szőnyeg", "fal", "festmény", "kép", "fénykép", "tükör", "ablak", "ajtó", "kémény",
    "tető", "erkély", "udvar", "kert", "fűtés", "világítás", "légkondi", "pára", "szellőzés", "páraelszívó",
    "szőnyegtisztító", "kávéfőző", "vízforraló", "hűtőszekrény", "mosógép", "mosogatógép", "villanytűzhely",
    "sütő", "mikrohullámú", "étkező", "nappali", "hálószoba", "fürdőszoba", "tusoló", "kád", "törölköző",
    "fogkefe", "ablak", "órája", "barack", "bevonat", "bél", "cseresznye", "dió", "elme", "felhő", "fűtés",
    "gomb", "gyűrű", "hajó", "halászat", "használat", "intézet", "jelszó", "jég", "jog", "karóra",
    "király", "kert", "korcsolya", "könyv", "málna", "méz", "minta", "nyelv", "orvos", "pad",
    "papír", "pecsét", "pénztárca", "saját", "sál", "színes", "szikla", "szomszéd", "szoba", "szél",
    "tél", "tűzhely", "zseb", "zsák", "tó", "vászon", "vereb", "máj", "keverék", "gomba",
    "kalap", "autóbusz", "só", "paradicsom", "barlang", "bögre", "szerszám", "felfedezés", "káposzta",
    "kávézó", "fűrész", "megálló", "autópálya", "sütő", "mosó", "szennyvíz", "csapda", "gépjármű",
    "elefánt", "giliszta", "festék", "virágcserép", "kutatás", "bőség", "mókus", "fa", "szökőár",
    "nyaraló", "róka", "oroszlán", "sáv", "sütemény", "köröm", "sárgarépa", "süti", "kefe", "játszótér",
    "tábla", "fényképezés", "táska", "pénz", "kereslet", "vihar", "fogás", "kerékpár", "lakás",
    "villamos", "mennyország", "bútor", "hálózat", "forrás", "kávé", "kamera", "bomba", "lakó",
    "számítógép", "kötél", "szendvics", "labda", "gyümölcslé", "bikini", "érem", "tehenek",
    "lapát", "tartós", "képződmény", "fehérje", "dinnye", "kézfej", "szemüveg", "kocsis",
    "üzlethelyiség", "bomba", "vélemény", "szivacs", "nyugalom", "tábla", "tető", "hóvirág",
    "kávéfőző", "máj", "zöldségek", "teáskanál", "élmény", "kapu", "színpad", "hegycsúcs",
    "tükör", "munkafolyamat", "szépség", "drágakő", "felszín", "üveg", "összeszerelés", "tudományos",
    "szélirány", "szobor", "kitöltés", "terv", "elmélet", "szabály", "hangverseny", "zár",
    "kő", "görögdinnye", "lazac", "betű", "fénykép", "gőz", "pohár", "nyaklánc", "tábor",
    "férfi", "pénzérme", "lufi", "rúzs", "alvás", "mosdó", "vizsga", "futás", "dísz", "keveredés",
    "motorkerékpár", "park", "tópart", "erdő", "folyó", "hegy", "domb", "sziget", "völgy", "szél", "fagy", "tavasz",
    "nyár", "ősz", "tél", "víz", "tenger", "eső", "hó", "fű", "virág", "fa", "levél", "ág", "virág", "madár", "béka",
    "rovar", "ló", "kutya", "macska", "elefánt", "oroszlán", "zebra", "teknős", "hal", "csirke", "pulyka", "tojás",
    "sajt", "túró", "kenyér", "pék", "gyümölcs", "zöldség", "alma", "körte", "barack", "cseresznye", "szilva",
    "dinnye", "szőlő", "paprika", "paradicsom", "hagyma", "fokhagyma", "káposzta", "uborka", "répa", "burgonya",
    "rizs", "hús", "csirke", "marha", "sertés", "hal", "sült", "leves", "pörkölt", "túrós csusza", "lángos", "hamburger",
    "pizza", "gyümölcsleves", "csokoládé", "sütemény", "torta", "fagylalt", "kávé", "tea", "víz", "üdítő", "bor", "sör",
    "pálinka", "víz", "kávézó", "étterem", "bolt", "piac", "pláza" };
            int index = random.Next(fikagolyo.Count);
            WordToGuess = fikagolyo[index].ToUpper();
            guessedLetters = new List<char>();
            wrongGuesses = 0;
            notWrongGuesses = 0;
            UpdateWordDisplay();
            ClearDrawing();
        }

        private void UpdateWordDisplay()
        {
            string displayWord = "";
            foreach (char c in WordToGuess)
            {
                displayWord += guessedLetters.Contains(c) ? c + " " : "_ ";
            }
            WordDisplay.Text = displayWord.Trim();
        }

        private void ClearDrawing()
        {
            Drawing.Children.Clear();
        }
        private void DrawHangman()
        {
            if (wrongGuesses == 1) allvany1();
            else if (wrongGuesses == 2) allvany2();
            else if (wrongGuesses == 3) allvany3();
            else if (wrongGuesses == 4) allvany4();
            else if (wrongGuesses == 5) allvany5();
            else if (wrongGuesses == 6) Head();
            else if (wrongGuesses == 7) Body();
            else if (wrongGuesses == 8) rightArm();
            else if (wrongGuesses == 9) leftArm();
            else if (wrongGuesses == 10) rightLeg();
            else if (wrongGuesses == 11)
            {
                leftLeg();
                MessageBox.Show("Nem találtad ki a szót.", "Game Over");
                MessageBox.Show(WordToGuess, "A szó nem volt más mint:");
                StopLoop();
                Application.Current.Shutdown();
            }
        }
        private async void StartBackgroundLoop()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            await Task.Run(() => BackgroundLoop(token));
        }
        private void BackgroundLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                string listContent = string.Join(", ", guessedLetters);
                Dispatcher.Invoke(() =>
                {
                    LettersContent.Text = listContent;
                });

                Thread.Sleep(100);
            }
        }
        private void StopLoop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
        private void Head()
        {
            Ellipse head = new Ellipse { Width = 20, Height = 20, Stroke = Brushes.Black, StrokeThickness = 2 };
            Canvas.SetLeft(head, 90);
            Canvas.SetTop(head, 10);
            Drawing.Children.Add(head);
        }
        private void Body()
        {
            Line body = new Line { X1 = 100, Y1 = 30, X2 = 100, Y2 = 100, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(body);
        }

        private void leftArm()
        {
            Line leftArm = new Line { X1 = 100, X2 = 70, Y1 = 50, Y2 = 70, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(leftArm);
        }
        private void rightArm()
        {
            Line rightArm = new Line { X1 = 100, X2 = 130, Y1 = 50, Y2 = 70, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(rightArm);
        }
        private void rightLeg()
        {
            Line rightLeg = new Line { X1 = 100, Y1 = 100, X2 = 130, Y2 = 130, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(rightLeg);
        }
        private void leftLeg()
        {
            Line leftLeg = new Line { X1 = 100, Y1 = 100, X2 = 70, Y2 = 130, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(leftLeg);
        }
        private void allvany1()
        {
            Line allvany1 = new Line { X1 = 150, Y1 = 180, X2 = 230, Y2 = 180, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(allvany1);
        }
        private void allvany2()
        {
            Line allvany2 = new Line { X1 = 200, Y1 = -30, X2 = 200, Y2 = 180, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(allvany2);
        }
        private void allvany3()
        {
            Line allvany3 = new Line { X1 = 100, Y1 = -30, X2 = 200, Y2 = -30, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(allvany3);
        }
        private void allvany4()
        {
            Line allvany4 = new Line { X1 = 170, Y1 = -30, X2 = 200, Y2 = 0, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(allvany4);
        }
        private void allvany5()
        {
            Line allvany5 = new Line { X1 = 100, Y1 = -30, X2 = 100, Y2 = 12, Stroke = Brushes.Black, StrokeThickness = 2 };
            Drawing.Children.Add(allvany5);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Drawing.Focus();
        }

        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Drawing.Focus();
                SubmitGuess_Click(sender, e);
            }
        }

        private void SubmitGuess_Click(object sender, RoutedEventArgs e)
        {
            char guessedLetter;

            if (char.TryParse(GuessInput.Text.ToUpper(), out guessedLetter))
            {
                if (char.IsDigit(guessedLetter))
                {
                    MessageBox.Show("Számot nem fogadok el, csak betűt.", "Hiba!");
                    return;
                }
                if (guessedLetters.Contains(guessedLetter))
                {
                    MessageBox.Show("Ezt a betűt már próbáltad!", "Hiba!");
                }
                else
                {
                    guessedLetters.Add(guessedLetter);

                    if (!WordToGuess.Contains(guessedLetter))
                    {
                        wrongGuesses++;
                        DrawHangman();
                    }

                    UpdateWordDisplay();
                }

                GuessInput.Clear();
            }

            if (WordToGuess.All(c => guessedLetters.Contains(c)))
            {
                MessageBox.Show("Gratulálok, kitaláltad!", "Nyertél!");
                StopLoop();
                Application.Current.Shutdown();
            }
        }

    }
}

