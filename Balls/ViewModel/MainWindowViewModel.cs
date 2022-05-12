using Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ModelAbstractApi ModelLayer;
        private int _NumberOfBalls;
        public int ballIndex;
        public int X { get
            {
                return ModelLayer.getX(ballIndex);
            } }
        public int Y
        {
            get
            {
                return ModelLayer.getY(ballIndex);
            }
        }
       
        public int Size { get
            {
                return ModelLayer.getSize(ballIndex);
            } }

        public MainWindowViewModel()
        {
            ModelLayer = ModelAbstractApi.CreateApi(400, 400);
            Scommand = new RelayCommand(Stop);
            Acommand = new RelayCommand(CreateBalls);
        }

        public ICommand Scommand
        { get; set; }

        public ICommand Acommand
        { get; set; }


        public int NumberOfBalls
        {
            get 
            { 
                return _NumberOfBalls; 
            }
            set
            {
                _NumberOfBalls = value;
                RaisePropertyChanged();
            }
        }

        public void CreateBalls()
        {
            ModelLayer.ModelBalls(NumberOfBalls);
        }
        //public BindableCollection<> balls { get; set; }

        private void Stop()
        {
            ModelLayer.Stop();
        }



    }
}
