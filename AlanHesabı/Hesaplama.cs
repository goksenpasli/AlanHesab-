using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AlanHesabı
{
    public class Hesaplama : InpcBase
    {
        private ObservableCollection<Point> noktalar = new ObservableCollection<Point>();

        private List<double> fontWidth = new List<double>();

        private ObservableCollection<double> alanlar = new ObservableCollection<double>();

        private double alan;

        private double toplamAlan;

        private double derinlik;

        private double birimFiyat;

        private ObservableCollection<Hesaplama> topluHesaplamaListe = new ObservableCollection<Hesaplama>();

        private Geometry geometryData;

        private Brush seçiliRenk;

        private Geometry sekil;

        public ObservableCollection<Point> Noktalar
        {
            get => noktalar;
            set
            {
                if (noktalar != value)
                {
                    noktalar = value;
                    OnPropertyChanged(nameof(Noktalar));
                }
            }
        }

        public Geometry Sekil
        {
            get { return sekil; }

            set
            {
                if (sekil != value)
                {
                    sekil = value;
                    OnPropertyChanged(nameof(Sekil));
                }
            }
        }

        public double Alan
        {
            get => alan;

            set
            {
                if (alan != value)
                {
                    alan = value;
                    OnPropertyChanged(nameof(Alan));
                }
            }
        }

        public double Derinlik
        {
            get { return derinlik; }

            set
            {
                if (derinlik != value)
                {
                    derinlik = value;
                    OnPropertyChanged(nameof(Derinlik));
                }
            }
        }

        public double ToplamAlan
        {
            get { return toplamAlan; }

            set
            {
                if (toplamAlan != value)
                {
                    toplamAlan = value;
                    OnPropertyChanged(nameof(ToplamAlan));
                }
            }
        }

        public double BirimFiyat
        {
            get { return birimFiyat; }

            set
            {
                if (birimFiyat != value)
                {
                    birimFiyat = value;
                    OnPropertyChanged(nameof(BirimFiyat));
                }
            }
        }

        public ObservableCollection<double> Alanlar
        {
            get { return alanlar; }

            set
            {
                if (alanlar != value)
                {
                    alanlar = value;
                    OnPropertyChanged(nameof(Alanlar));
                }
            }
        }

        public Brush SeçiliRenk
        {
            get { return seçiliRenk; }

            set
            {
                if (seçiliRenk != value)
                {
                    seçiliRenk = value;
                    OnPropertyChanged(nameof(SeçiliRenk));
                }
            }
        }

        public Geometry GeometryData
        {
            get { return geometryData; }

            set
            {
                if (geometryData != value)
                {
                    geometryData = value;
                    OnPropertyChanged(nameof(GeometryData));
                }
            }
        }

        public ObservableCollection<Hesaplama> TopluHesaplamaListe
        {
            get { return topluHesaplamaListe; }

            set
            {
                if (topluHesaplamaListe != value)
                {
                    topluHesaplamaListe = value;
                    OnPropertyChanged(nameof(TopluHesaplamaListe));
                }
            }
        }

        public List<double> FontWidth
        {
            get => fontWidth; set
            {
                if (fontWidth != value)
                {
                    fontWidth = value;
                    OnPropertyChanged(nameof(FontWidth));
                }
            }
        }

        public Hesaplama()
        {
            for (double i = 1; i < 256; i++)
            {
                FontWidth.Add(i);
            }

            AlanHesap = new RelayCommand(parameter =>
            {
                var hesaplama = parameter as Hesaplama;
                hesaplama.Alanlar.Add(hesaplama.Alan);
                hesaplama.ToplamAlan = hesaplama.Alanlar.Sum();
                hesaplama.TopluHesaplamaListe.Add(new Hesaplama() { SeçiliRenk = hesaplama.SeçiliRenk, GeometryData = hesaplama.GeometryData, Alan = hesaplama.Alan, BirimFiyat = hesaplama.BirimFiyat, Derinlik = hesaplama.Derinlik });
            }, parameter => true);

            YeniHesap = new RelayCommand(parameter =>
            {
                var hesaplama = parameter as Hesaplama;
                hesaplama.GeometryData = null;
                hesaplama.Alan = 0;
                hesaplama.Noktalar = new System.Collections.ObjectModel.ObservableCollection<Point>();
            }, parameter => true);

            TopluHesaplama = new RelayCommand(parameter =>
            {
                var hesaplama = parameter as Hesaplama;
                var sonuç = hesaplama.Derinlik * hesaplama.Alan * hesaplama.BirimFiyat;
                MessageBox.Show($"Toplam Tutar {sonuç:C}", "Alan Hesaplama");
            }, parameter => true);
        }

        public ICommand AlanHesap { get; }

        public ICommand YeniHesap { get; }

        public ICommand TopluHesaplama { get; }
    }
}