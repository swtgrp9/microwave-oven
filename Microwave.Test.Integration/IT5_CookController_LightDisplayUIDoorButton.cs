using System;
using System.Runtime.InteropServices;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    public class IT5_CookController_LightDisplayUIDoorButton
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
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _light = Substitute.For<ILight>();  //Faked for at bryde loop i dep. tree

            //top
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();



            //includes
            _display = new Display(_output);

            //Sut
           
            _cookController = new CookController(_timer, _display, _powerTube);
            _iut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _iut;

    


        }

        [Test]
        public void UserInterfaceCallsCookController_TurnsOnPowerTube()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Received(1).Start(Arg.Any<int>());
            _powerTube.Received(1).TurnOn(Arg.Any<int>());
        }

        [Test]
        public void UserInterfaceCallsCookController_TurnsOffPowerTube()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            _timer.Received(1).Stop();
            _powerTube.Received(1).TurnOff();
        }

        [Test]
        public void UserInterfaceCalledByCookController()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.Event();
            _light.Received(1).TurnOff();
        }

       
        [Test]
        public void CookControllerCallsDisplay()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.TimeRemaining.Returns(55);
            _timer.TimerTick += Raise.Event();

            //Thread.Sleep(6000);

            _output.Received(1).OutputLine(Arg.Is("Display shows: 00:55"));
        }
    }
}
