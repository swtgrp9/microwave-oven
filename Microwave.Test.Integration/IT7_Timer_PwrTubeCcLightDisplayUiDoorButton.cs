using System;
using System.Runtime.InteropServices;
using System.Threading;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    public class IT7_Timer_PwrTubeCcLightDisplayUiDoorButton
    {
        //Top
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;

        //stubs

        private IOutput _output;

        //Includes
        private CookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private UserInterface _iut;

        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            //stubs
            _output = Substitute.For<IOutput>();
            _light = Substitute.For<ILight>();


            //top
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _timer = new Timer();



            //includes
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);


            //Sut

            _cookController = new CookController(_timer, _display, _powerTube);
            _iut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _iut;
        }

        [Test]
        public void Start_Cooking_Timer_Start()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            
            Thread.Sleep(2000);

            _output.Received(1).OutputLine(Arg.Is("Display shows: 00:58"));


        }

        [Test]
        public void Door_Opened_Timer_Stopped()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Thread.Sleep(2000);

            _door.Open();
            _door.Close();

            _output.Received(1).OutputLine(Arg.Is("Display shows: 01:00"));
        }

        [Test]
        public void Timer_Expired()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Thread.Sleep(60000);

            _output.Received().OutputLine(Arg.Is("Display shows: 01:00"));
            _output.Received().OutputLine(Arg.Is("PowerTube works with 50 W"));
        }

    }
}
