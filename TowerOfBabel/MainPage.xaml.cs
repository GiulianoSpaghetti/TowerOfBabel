using org.altervista.numerone.framework;

namespace TowerOfBabel
{
    public partial class MainPage : ContentPage
    {
        private int a,b;
        private Image img;
        private UInt16[] vettore;
        private UInt16 c;
        private ulong mosse = 0;
        private TapGestureRecognizer gesture, gesture1;
        public MainPage()
        {
            UInt16 a = 0, b = 0, c=10;
            InitializeComponent();
            vettore = new UInt16[27];
            ElaboratoreCarteBriscola e = new ElaboratoreCarteBriscola(true, 9, 1, 9);
            Mazzo m = new Mazzo(e);
            org.altervista.numerone.framework.solitario.CartaHelper chs = new org.altervista.numerone.framework.solitario.CartaHelper();
            Carta.Inizializza(10, chs, "", "", "", "");
            mnFile.Text=App.d["File"] as string;
            mnNuovaPartita.Text=App.d["NuovaPartita"] as string;
            mnEsci.Text=App.d["Esci"] as string;
            mnInfo.Text=App.d["Info"] as string;
            mnInformazioni.Text=App.d["Informazioni"] as string;
            gesture = new TapGestureRecognizer();
            gesture.Tapped += Image_Tapped;
            gesture1 = new TapGestureRecognizer();
            gesture1.Tapped += Image_Tapped1;
            for (UInt16 i = 0; i < 9; i++)
            {
                c = m.GetCarta();

                vettore[a * 9 + b] = c;
                img = (Image)FindByName($"carta{c}");
                img.GestureRecognizers.Add(gesture);
                Applicazione.SetColumn(img, a);
                Applicazione.SetRow(img, b);
                img.IsVisible = true;
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            c = 10;
            for (UInt16 j = 0; j < 3; j++)
                for (UInt16 i = 3; i < 9; i++)
                    vettore[j * 9 + i] = c++;
            for (UInt16 i = 10; i < 28; i++) { 
                img = (Image)FindByName($"carta{i}");
                img.GestureRecognizers.Add(gesture1);
            }
        }

        private void Image_Tapped(object Sender, EventArgs arg)
        {
            Image im;
            UInt16 i, j;
            img = (Image)Sender;
            for (i = 1; i < 9; i++)
            {
                im = (Image)FindByName("carta" + i);
                if (im.Id == img.Id)
                {
                    break;
                }
            }
            for (j=0; j<28; j++)
            {
                if (vettore[j] == i)
                    break;
            }
            if (vettore[j + 1] > 9)
                a = j;
            else
                img = null;
        }
        private void Image_Tapped1(object Sender, EventArgs arg)
        {
            bool found = false;
            UInt16 i = 0, j;
            if (img == null)
                return;
            Image im0 = (Image)Sender, im1=null;
            for (i = 10; i < 28 && !found; i++)
            {
               im1 = (Image)FindByName("carta" + i);
               if (im0.Id == im1.Id)
               {
                   found = true;
               }
            }
            found = false;
            --i;
            for (j = 0; j < 27 && !found; j++)
                if (vettore[j] == i)
                    found = true;
            --j;
            --j;
            b = j;
            if (b == UInt16.MaxValue)
            {
                c = vettore[0];
                vettore[0] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1, 3);
                Applicazione.SetRow(im1, 0);
                Applicazione.SetRow(img, 0);
                Applicazione.SetColumn(img, 0);
                Applicazione.SetColumn(im1, a / 9);
                Applicazione.SetRow(im1, a % 9);
            }
            else if (b %9==8)
            {
                c = vettore[b+1];
                vettore[b+1] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1, 3);
                Applicazione.SetRow(im1, 0);
                Applicazione.SetRow(img, 0);
                Applicazione.SetColumn(img, (b+1)/9);
                Applicazione.SetColumn(im1, a / 9);
                Applicazione.SetRow(im1, a % 9);
            }
            else if (vettore[a] < vettore[b] && vettore[b]<10)
            {
                c = vettore[b + 1];
                vettore[b + 1] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1,3);
                Applicazione.SetRow(im1, 0);
                if ((b + 1) % 9 == 8)
                {
                    Applicazione.SetRow(img, 8);
                    Applicazione.SetColumn(img, (b+1)/9);
                    bool continua = true;
                    UInt16 x = 0;
                    if (vettore[0] == 9)
                        i = 0;
                    else if (vettore[9] == 9)
                        i = 9;
                    else if (vettore[18] == 9)
                        i = 18;
                    for (x = 0; x < 8 && continua; x++)
                        continua = vettore[i + x] > vettore[i + x + 1];
                    if (continua)
                    {
                        Navigation.PushAsync(new GreetingsPage(mosse));
                        NuovaPartita();
                    }
                }
                else
                {
                    Applicazione.SetRow(img, (b+1)%9);
                    Applicazione.SetColumn(img, (b+1)/9);
                }
                Applicazione.SetColumn(im1, a/9);
                Applicazione.SetRow(im1, a%9);
                img = null;
                mosse++;
            }
        }

        private void OnFine_Click(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }


        private void OnNuovaPartita_Click(object sender, EventArgs e)
        {
            NuovaPartita();
        }
        private void OnInfo_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }
        private void NuovaPartita()
        {
            int a = 0, b = 0;
            mosse = 0;
            ElaboratoreCarteBriscola e = new ElaboratoreCarteBriscola(true, 9, 1, 9);
            Mazzo m = new Mazzo(e);

            UInt16 c;
            Image img;
            for (UInt16 i = 0; i < 9; i++)
            {
                c = m.GetCarta();

                vettore[a * 9 + b] = c;
                img = (Image)FindByName($"carta{c}");
                Applicazione.SetColumn(img, a);
                Applicazione.SetRow(img, b);
                img.IsVisible = true;
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            c = 10;
            for (UInt16 j = 0; j < 3; j++)
                for (UInt16 i = 3; i < 9; i++)
                    vettore[j * 9 + i] = c++;
            a = 3; b = 0;
            for (UInt16 i = 10; i < 28; i++)
            {
                img = (Image)FindByName($"carta{i}");
                Applicazione.SetColumn(img, b);
                Applicazione.SetRow(img, a);
                if (a==8)
                {
                    a = 3;
                    b++;
                }
                else 
                    a++;
            }
        }
    }
}
